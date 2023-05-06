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

        Game.ClearColor = new Color(70, 70, 70);
        Camera.Target = Player;

        base.Initialize();
    }

    public void CreateSystems()
    {
        AddUpdateSystem(new CameraZoomControlSystem());
        AddUpdateSystem(new EntityControlSystem(Player));

        AddUpdateSystem(new MovementSystem());
        AddUpdateSystem(new CollisionSystem());

        AddRenderSystem(new RenderSystem());
    }

    public void CreateEntities()
    {
        LevelFactory factory = new(this);
        WorldGenerator generator = new(factory);

        Player = factory.CreatePlayer(new Vector2(100.0f, 0.0f));
        generator.Generate();
    }
}
