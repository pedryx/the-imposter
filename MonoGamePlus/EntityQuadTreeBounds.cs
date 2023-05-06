using Arch.Core;
using Arch.Core.Extensions;

using Microsoft.Xna.Framework;

using MonoGamePlus.Components;

using UltimateQuadTree;

namespace MonoGamePlus;
internal class QuadTreeEntityBounds : IQuadTreeObjectBounds<Entity>
{
    private readonly GameState gameState;

    public QuadTreeEntityBounds(GameState gameState)
    {
        this.gameState = gameState;
    }

    public Rectangle GetBounds(Entity entity)
    {
        Transform transform = entity.Get<Transform>();
        Collider collider = entity.Get<Collider>();

        return new Rectangle()
        {
            Location = (transform.Position - collider.Size / 2.0f + gameState.WorldSize / 2.0f).ToPoint(),
            Size = collider.Size.ToPoint(),
        };
    }

    public double GetBottom(Entity entity)
        => GetBounds(entity).Bottom;

    public double GetLeft(Entity entity)
        => GetBounds(entity).Left;

    public double GetRight(Entity entity)
        => GetBounds(entity).Right;

    public double GetTop(Entity entity)
        => GetBounds(entity).Top;
}
