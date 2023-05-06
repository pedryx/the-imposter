using Microsoft.Xna.Framework;

using MonoGamePlus;
using MonoGamePlus.Resources;
using MonoGamePlus.UI.Elements;

using System;

using TheImposter.GameStates.Level;

namespace TheImposter.GameStates;
internal class StageWinState : GameState
{
    private readonly Statistics statistics;
    private readonly PlayerUpgrades upgrades;
    private readonly int stage;

    public StageWinState(Statistics statistics, PlayerUpgrades upgrades, int stage)
    {
        this.statistics = statistics;
        this.upgrades = upgrades;
        this.stage = stage;
    }

    protected override void Initialize()
    {
        UILayer.AddElement(new Image(new Sprite(Game.Textures["dark background"])
        {
            Origin = Vector2.Zero,
            Position = new Vector2(-100.0f)
        }));

        UILayer.AddElement(new Label(new SpriteText(Game.Fonts["Storm Gust;128"], "Stage Complete", Color.White))
        {
            Offset = new Vector2(Game.Resolution.X / 2.0f, 100.0f),
        });

        StackPanel panel = new(Game.Resolution / 2.0f + new Vector2(0.0f, 100.0f))
        {
            Padding = 40.0f,
        };
        UILayer.AddElement(panel);
        panel.Add(CreateLabel("PICK  AN  UPGRADE:", 32));
        panel.Add(CreateButton("MOVE  SPEED", () =>
        {
            upgrades.UpgradeMoveSpeed();
            NextStage();
        }));

        base.Initialize();
    }

    private void NextStage()
    {
        Game.RemoveGameState(this);
        Game.AddGameState(new LevelState(statistics, upgrades, stage + 1));
    }

    private Label CreateLabel(string text, int size = 48)
        => new(new SpriteText(Game.Fonts[$"Curse of the Zombie;{size}"], text, Color.Gray));

    private Button CreateButton(string text, Action action)
    {
        Button button = new();

        button.Label.SpriteText = new SpriteText(Game.Fonts["Curse of the Zombie;64"], text, Color.Gray);
        button.HoverColor = Color.Yellow;
        button.OnClick += (sender, e) => { action(); };

        return button;
    }
}
