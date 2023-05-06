using Microsoft.Xna.Framework;

using MonoGamePlus;
using MonoGamePlus.Resources;
using MonoGamePlus.UI;
using MonoGamePlus.UI.Elements;

using System;

using TheImposter.GameStates.Level;

namespace TheImposter.GameStates;
internal class TitleState : GameState
{
    protected override void Initialize()
    {
        UILayer.AddElement(new Image(new Sprite(Game.Textures["dark background"])
        {
            Origin = Vector2.Zero,
            Position = new Vector2(-100.0f)
        }));

        UILayer.AddElement(new Label(new SpriteText(Game.Fonts["Storm Gust;200"], "The Imposter", Color.White))
        {
            Offset = new Vector2(Game.Resolution.X / 2.0f, 200.0f),
        });

        StackPanel panel = new(Game.Resolution / 2.0f + new Vector2(0.0f, 150.0f))
        {
            Padding = 40.0f,
        };
        UILayer.AddElement(panel);
        panel.Add(CreateButton("PLAY", () =>
        {
            Game.RemoveGameState(this);
            Game.AddGameState(new LevelState());
        }));
        panel.Add(CreateButton("EXIT", () =>
        {
            Game.Exit();
        }));

        base.Initialize();
    }

    private Button CreateButton(string text, Action action)
    {
        Button button = new();

        button.Label.SpriteText = new SpriteText(Game.Fonts["Curse of the Zombie;64"], text, Color.Gray);
        button.HoverColor = Color.Yellow;
        button.OnClick += (sender, e) => { action(); };

        return button;
    }
}
