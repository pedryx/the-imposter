using Arch.Core;

using Microsoft.Xna.Framework;

using MonoGamePlus;
using MonoGamePlus.Systems;

using TheImposter.Systems;

namespace TheImposter.GameStates.Level;
internal class LevelState : GameState
{
    private EntityControlSystem controlSystem;

    private Graph graph;

    public Entity Player { get; private set; }
    public Entity Imposter { get; private set; }

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
        AddUpdateSystem(new PlayerControlSystem(this));

        AddRenderSystem(new RenderSystem());
    }

    public void CreateEntities()
    {
        LevelFactory factory = new(this);
        WorldGenerator generator = new(factory, this);

        generator.Generate(100);

        graph = generator.Graph;
        Imposter = generator.Imposter;

        Player = factory.CreatePlayer(generator.Spawn);
    }
}
