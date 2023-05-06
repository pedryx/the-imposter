using Arch.Core;
using Arch.Core.Extensions;

using Microsoft.Xna.Framework;

using MonoGamePlus;
using MonoGamePlus.Components;
using MonoGamePlus.Resources;

namespace TheImposter.GameStates.Level;
internal class LevelFactory
{
    private readonly MGPGame game;
    private readonly World ecsWorld;

    public LevelFactory(GameState gameState)
    {
        game = gameState.Game;
        ecsWorld = gameState.ECSWorld;
    }

    private Entity CreateCharacter(Vector2 position, Color color)
        => ecsWorld.Create(
            new Transform(position),
            new Appearance(new Sprite(game.Textures.CreateCircle(30, color))),
            new Movement());

    public Entity CreateNPC(Vector2 position, Color color)
    {
        var npc = CreateCharacter(position, color);

        npc.Add(new PathFollow());
        npc.Get<Movement>().Speed = 150.0f;

        return npc;
    }

    public Entity CreatePlayer(Vector2 position)
    {
        var player = CreateCharacter(position, Color.Red);

        player.Add(new Collider(player.Get<Appearance>().Sprite.GetSize())
        {
            Layer = (uint)CollisionLayers.Player,
            CollisionLayer = (uint)CollisionLayers.Walls,
        });
        player.Add<Foreground>();

        return player;
    }

    public Entity CreateImposter(Vector2 position)
    {
        var imposter = CreateNPC(position, Color.Brown);

        imposter.Add<Foreground>();

        return imposter;
    }

    public Entity CreateWall(Vector2 start, Vector2 end, float width)
    {
        Vector2 size = start.X == end.X ? new Vector2(width, end.Y - start.Y + width)
            : new Vector2(end.X - start.X + width, width);

        return ecsWorld.Create(
            new Transform(start - new Vector2(width) / 2.0f + size / 2.0f),
            new Appearance(new Sprite(game.Textures.CreateRectangle(size, Color.Black))),
            new Collider(size)
            {
                Layer = (uint)CollisionLayers.Walls
            });
    }
}
