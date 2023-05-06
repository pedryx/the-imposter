using Arch.Core;
using Arch.Core.Extensions;

using Microsoft.Xna.Framework;

using MonoGamePlus.Components;
using MonoGamePlus.Events.Events;

using UltimateQuadTree;

namespace MonoGamePlus.Systems;
public class CollisionSystem : GameSystem<Transform, Collider, Movement>
{
    private readonly QueryDescription query;

    private QuadTree<Entity> quadTree;

    public CollisionSystem()
    {
        query = new QueryDescription().WithAll<Transform, Collider>();
    }

    protected override void PreUpdate(float elapsed)
    {
        quadTree = new QuadTree<Entity>(
            GameState.WorldSize.X,
            GameState.WorldSize.Y,
            new QuadTreeEntityBounds(GameState));

        ECSWorld.Query(query, (in Entity entity) =>
        {
            quadTree.Insert(entity);
        });

        base.PreUpdate(elapsed);
    }

    protected override void Update(
        float elapsed,
        in Entity entity1,
        ref Transform transform,
        ref Collider collider,
        ref Movement movement)
    {
        var entities = quadTree.GetNearestObjects(entity1);
        foreach (var entity2 in entities)
        {
            HandleCollisions(elapsed, entity1, entity2);
        }

        base.Update(elapsed, entity1, ref transform, ref collider, ref movement);
    }

    private void HandleCollisions(float elapsed, Entity entity1, Entity entity2)
    {
        if (entity1.Id == entity2.Id)
            return;

        if (!Intersects(entity1, entity2))
            return;

        ref Transform transform1 = ref entity1.Get<Transform>();
        Transform transform2 = entity2.Get<Transform>();
        Collider collider1 = entity1.Get<Collider>();
        Collider collider2 = entity2.Get<Collider>();

        if ((collider1.ReactionLayer & collider2.Layer) > 0)
            GameState.Events.OnCollision?.Invoke(this, new CollisionEventArgs(entity1, entity2));

        if ((collider1.CollisionLayer & collider2.Layer) > 0)
        {
            if (entity1.Has<Movement>())
            {
                Movement movement = entity1.Get<Movement>();

                transform1.Position -= MathUtils.AngleToVector(movement.Direction) * movement.Speed * elapsed * Game.Speed;
            }
        }
    }

    private static bool Intersects(Entity entity1, Entity entity2)
    {
        Transform transform1 = entity1.Get<Transform>();
        Transform transform2 = entity2.Get<Transform>();
        Collider collider1 = entity1.Get<Collider>();
        Collider collider2 = entity2.Get<Collider>();

        var rectangle1 = new Rectangle()
        {
            Location = (transform1.Position - collider1.Size / 2.0f).ToPoint(),
            Size = collider1.Size.ToPoint()
        };
        var rectangle2 = new Rectangle()
        {
            Location = (transform2.Position - collider2.Size / 2.0f).ToPoint(),
            Size = collider2.Size.ToPoint()
        };

        return rectangle1.Intersects(rectangle2);
    }
}
