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
    public Statistics Statistics { get; private set; } = new();
    public PlayerUpgrades Upgrades { get; private set; } = new();
    public int Stage { get; private set; } = 1;

    public LevelState() { }

    public LevelState(Statistics statistics, PlayerUpgrades playerUpgrades, int stage)
    {
        Statistics = statistics;
        Upgrades = playerUpgrades;
        Stage = stage;
    }

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
            Speed = 500.0f + Upgrades.MoveSpeed,
        };

        AddUpdateSystem(new TimePlayedTrackingSystem(this));
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
