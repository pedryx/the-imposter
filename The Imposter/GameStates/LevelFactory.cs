using Arch.Core;

using Microsoft.Xna.Framework;

using MonoGamePlus;
using MonoGamePlus.Components;
using MonoGamePlus.Resources;

namespace TheImposter.GameStates;
internal class LevelFactory
{
    private readonly MGPGame game;
    private readonly GameState gameState;
    private readonly World ecsWorld;

    public LevelFactory(GameState gameState)
    {
        game = gameState.Game;
        this.gameState = gameState;
        ecsWorld = gameState.ECSWorld;
    }

    public Entity CreatePlayer(Vector2 position)
        => ecsWorld.Create(
            new Transform(position),
            new Appearance(new Sprite(game.Textures.CreateCircle(30, Color.Red))),
            new Movement(),
            new Collider(new Vector2(60))
            {
                Layer = (uint)CollisionLayers.Player,
                CollisionLayer = (uint)CollisionLayers.Walls,
            });

    public Entity CreateWall(Vector2 start, Vector2 end, float width)
    {
        Vector2 size = start.X == end.X ? new Vector2(width, end.Y - start.Y + width) : new Vector2(end.X - start.X + width, width);

        return ecsWorld.Create(
            new Transform(start - new Vector2(width) / 2.0f + size / 2.0f),
            new Appearance(new Sprite(game.Textures.CreateRectangle(size, Color.Black))),
            new Collider(size)
            {
                Layer = (uint)CollisionLayers.Walls
            });
    }
}
