using Arch.Core;

using Microsoft.Xna.Framework;

using MonoGamePlus;
using MonoGamePlus.Systems;

namespace TheImposter.GameStates;
internal class LevelState : GameState
{
    private EntityControlSystem controlSystem;

    private Graph graph;

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
        controlSystem = new EntityControlSystem(Player)
        {
            Speed = 500.0f,
        };

        AddUpdateSystem(new CameraZoomControlSystem());
        AddUpdateSystem(controlSystem);

        AddUpdateSystem(new RandomGraphWalkSystem(graph));
        AddUpdateSystem(new MovementSystem());
        AddUpdateSystem(new CollisionSystem());

        AddRenderSystem(new RenderSystem());
    }

    public void CreateEntities()
    {
        LevelFactory factory = new(this);
        WorldGenerator generator = new(factory, this);

        Player = factory.CreatePlayer(new Vector2(100.0f, 0.0f));
        graph = generator.Generate(100);
    }
}
