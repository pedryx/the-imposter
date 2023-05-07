using Arch.Core;
using Arch.Core.Extensions;

using Microsoft.Xna.Framework;

using MonoGamePlus;
using MonoGamePlus.Components;
using MonoGamePlus.Resources;
using MonoGamePlus.Systems;
using MonoGamePlus.UI;
using MonoGamePlus.UI.Elements;

using System.Collections.Generic;

using TheImposter.Systems;

namespace TheImposter.GameStates.Level;
internal class LevelState : GameState
{
    private const float maxImposterDistance = 100.0f;
    private const float playerBaseSpeed = 150.0f;
    private const int npcCount = 50;
    private const float time = 2.0f * 60.0f;

    public const int FinalStage = 12;
    public const float StartVisibility = 0.7f;
    public const float StartRadius = 200.0f;

    private readonly Color clearColor = new(70, 70, 70);

    private List<Entity> imposters;
    private EntityControlSystem controlSystem;
    // TODO: remove FPS counter
    private FPSCounterSystem fpsCounterSystem;
    private Label imposterCountLabel;
    private LevelFactory factory;

    private Graph graph;

    public Entity Player { get; private set; }
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

    public void TryFindImposter()
    {
        Vector2 playerPosition = Player.Get<Transform>().Position;

        for (int i = 0; i < imposters.Count; i++)
        {
            Vector2 imposterPosition = imposters[i].Get<Transform>().Position;
            float distance = Vector2.Distance(playerPosition, imposterPosition);

            if (distance <= maxImposterDistance)
            {
                Statistics.ImpostersFound++;
                ECSWorld.Destroy(imposters[i]);
                imposters.RemoveAt(i);
                UpdateImposterCountLabel();

                if (imposters.Count == 0)
                {
                    Statistics.CompletedStages++;

                    Game.RemoveGameState(this);
                    if (Stage == FinalStage)
                    {
                        Game.AddGameState(new GameOverState(Statistics, "YOU  HAVE  COMPLETED  ALL  STAGES!", true));
                    }
                    else
                    {
                        Game.AddGameState(new StageWinState(Statistics, Upgrades, Stage));
                    }
                }
                else
                {
                    Game.Sounds["twoTone2"].Play(0.3f, 0.0f, 0.0f);
                    Camera.Shake(0.25f, 5.0f);
                }
                
                return;
            }
        }

        Game.RemoveGameState(this);
        Game.AddGameState(new GameOverState(Statistics, "THAT  WAS  NOT  THE  IMPOSTER!"));
    }

    protected override void Initialize()
    {
        Music.Play(Game);

        CreateEntities();
        CreateSystems();
        CreateUI();

        Game.ClearColor = clearColor;
        Camera.Target = Player;

        PauseDialogState dialog = Dialogs.GetDialog(Stage);
        if (dialog != null)
            Game.AddGameState(dialog);

        base.Initialize();
    }

    private void CreateSystems()
    {
        controlSystem = new EntityControlSystem(Player)
        {
            Speed = playerBaseSpeed + Upgrades.MoveSpeed,
        };

        fpsCounterSystem = new FPSCounterSystem();

        DelayedStartSystem delayedStartSystem = new(0.15f);
        delayedStartSystem.OnDelayedStart += (sender, e) => LevelMusic.Play(Game);

        AddUpdateSystem(new TimePlayedTrackingSystem(this));
        AddUpdateSystem(new CameraZoomControlSystem());
        AddUpdateSystem(controlSystem);
        AddUpdateSystem(new RandomGraphWalkSystem(graph));
        AddUpdateSystem(new MovementSystem());
        AddUpdateSystem(new CollisionSystem());
        AddUpdateSystem(new PlayerControlSystem(this));
        AddUpdateSystem(delayedStartSystem);
        AddUpdateSystem(new NoiseSystem(Player, maxImposterDistance / 2.0f));
        AddUpdateSystem(new FogSystem(factory));

        AddUpdateSystem(fpsCounterSystem);

        AddUpdateSystem(new AnimationOrientationSystem());
        AddUpdateSystem(new AnimationSystem());
        AddRenderSystem(new RenderSystem());
    }

    private void CreateEntities()
    {
        factory = new(this);
        WorldGenerator generator = new(factory, this);

        generator.Generate(
            npcCount: npcCount,
            imposterCount: Stage / 2 + 1,
            impostersClothes: Stage == 3 || Stage == 4 || Stage >= 7,
            imposterSkeleton: Stage < 5,
            imposterMovement: Stage != 7 && Stage != 8,
            imposterAnimation: Stage != 9 && Stage != 10,
            imposterNoise: Stage >= 11);

        graph = generator.Graph;
        imposters = new List<Entity>(generator.Imposters);
        Player = factory.CreatePlayer(generator.Spawn);

        factory.CreateVisibility(StartRadius + Upgrades.LightRadius);
        factory.CreateDarkness(StartVisibility - Upgrades.LightIntensity);
    }

    private void CreateUI()
    {
        Timer timer = new()
        {
            Label = new Label(new SpriteText(Game.Fonts["Curse of the Zombie;46"], "00:00", Color.White)),
            Offset = new Vector2(Game.Resolution.X / 2.0f, 60.0f),
            Time = time,
        };
        timer.OnFinish += (sender, e) =>
        {
            Game.RemoveGameState(this);
            Game.AddGameState(new GameOverState(Statistics, "TIME  IS  UP!"));
        };
        UILayer.AddElement(timer);

        UILayer.AddElement(new Label(
            new SpriteText(Game.Fonts["Curse of the Zombie;32"],
            Stage.ToString(),
            Color.White))
        {
            Offset = new Vector2(Game.Resolution.X / 2.0f, timer.Label.Size.Y + timer.Offset.Y + 10.0f),
        });

        if (Stage >= 2)
        {
            imposterCountLabel = new Label(new SpriteText(Game.Fonts["Curse of the Zombie;32"], "IMPOSTERS:  000"));
            imposterCountLabel.Offset = new Vector2(
                imposterCountLabel.SpriteText.GetSize().X / 2.0f + 20.0f,
                10.0f + Game.Resolution.Y - imposterCountLabel.SpriteText.GetSize().Y);
            UpdateImposterCountLabel();
            UILayer.AddElement(imposterCountLabel);
        }
    }

    protected override void Destroy()
    {
        LevelMusic.Stop();

        base.Destroy();
    }

    private void UpdateImposterCountLabel()
    {
        if (imposterCountLabel == null)
            return;

        imposterCountLabel.SpriteText.Text = "IMPOSTERS:  " + imposters.Count;
    }
}
