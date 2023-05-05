using Arch.Core;

using Microsoft.Xna.Framework;

using MonoGamePlus;
using MonoGamePlus.Components;
using MonoGamePlus.Resources;
using MonoGamePlus.Systems;

namespace TheImposter.GameStates;
internal class LevelState : GameState
{
    public Entity Player { get; private set; }

    protected override void Initialize()
    {
        CreateEntities();
        CreateSystems();

        Camera.Target = Player;

        base.Initialize();
    }

    public void CreateSystems()
    {
        AddUpdateSystem(new CameraZoomControlSystem());
        AddUpdateSystem(new EntityControlSystem(Player));

        AddUpdateSystem(new MovementSystem());

        AddRenderSystem(new RenderSystem());
    }

    public void CreateEntities()
    {
        Player = ECSWorld.Create(
            new Transform(new Vector2(0.0f, 0.0f)),
            new Appearance(new Sprite(Game.Textures.CreateCircle(30, Color.Pink))),
            new Movement());

        ECSWorld.Create(
            new Transform(new Vector2(0.0f, 0.0f)),
            new Appearance(new Sprite(Game.Textures.CreateCircle(150, Color.Red))));

        ECSWorld.Create(
            new Transform(new Vector2(0, 0)),
            new Appearance(new Sprite(Game.Textures.CreateCircle(50, Color.Blue))));
        ECSWorld.Create(
            new Transform(new Vector2(100, 0)),
            new Appearance(new Sprite(Game.Textures.CreateCircle(50, Color.Yellow))));
        ECSWorld.Create(
            new Transform(new Vector2(100, 100)),
            new Appearance(new Sprite(Game.Textures.CreateCircle(50, Color.Green))));
    }
}
