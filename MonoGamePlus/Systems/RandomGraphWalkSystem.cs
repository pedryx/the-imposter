using Arch.Core;

using Microsoft.Xna.Framework;

using MonoGamePlus.Components;

namespace MonoGamePlus.Systems;
public class RandomGraphWalkSystem : GameSystem<Transform, Movement, PathFollow>
{
    private const float waitMin = 1.0f;
    private const float waitMax = 10.0f;

    private readonly Graph graph;

    public RandomGraphWalkSystem(Graph graph)
    {
        this.graph = graph;
    }

    protected override void Update(
        float elapsed,
        in Entity entity,
        ref Transform transform,
        ref Movement movement,
        ref PathFollow pathFollow)
    {
        if (pathFollow.WaitTime > 0.0f)
        {
            pathFollow.WaitTime -= elapsed;

            if (pathFollow.WaitTime < 0.0f)
                movement.Speed = pathFollow.Speed;

            return;
        }

        if (pathFollow.Path == null)
        {
            AssignPath(ref transform, ref movement, ref pathFollow);
        }

        float distance = Vector2.Distance(transform.Position, pathFollow.Path[pathFollow.PathIndex]);
        if (distance < movement.Speed * elapsed * Game.Speed)
        {
            transform.Position = pathFollow.Path[pathFollow.PathIndex];

            pathFollow.PathIndex++;
            if (pathFollow.PathIndex == pathFollow.Path.Length)
            {
                pathFollow.WaitTime = Game.Random.NextSingle(waitMin, waitMax);
                pathFollow.Speed = movement.Speed;
                movement.Speed = 0.0f;

                AssignPath(ref transform, ref movement, ref pathFollow);
            }
            movement.Direction = MathUtils.VectorToAngle(pathFollow.Path[pathFollow.PathIndex] - transform.Position);
        }

        base.Update(elapsed, entity, ref transform, ref movement, ref pathFollow);
    }

    private void AssignPath(ref Transform transform, ref Movement movement, ref PathFollow pathFollow)
    {
        pathFollow.PathIndex = 1;

        Point destination;
        do
        {
            destination = graph.GetRandomNode(Game.Random);
        }
        while (transform.Position.ToPoint() == destination);

        pathFollow.Path = graph.FindPath(transform.Position.ToPoint(), destination);
        movement.Direction = MathUtils.VectorToAngle(pathFollow.Path[pathFollow.PathIndex] - transform.Position);
    }
}
