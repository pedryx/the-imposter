using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGamePlus;
using MonoGamePlus.Resources;
using MonoGamePlus.UI.Elements;

using System;

namespace TheImposter.GameStates;
internal class PauseDialogState : GameState
{
    private const string confirmText = "OK";
    private const string fontName = "The Macabre";
    private const float lineWidth = 400;
    private const string dialogTextureName = "PauseMenu";

    private readonly Color color = Color.White;

    private string message;
    private SpriteFont messageFont;
    private SpriteFont confirmFont;

    public PauseDialogState(string message)
    {
        this.message = message;
    }

    protected override void Initialize()
    {
        foreach (var state in Game.ActiveStates)
        {
            if (state != this)
                state.Enable = false;
        }

        messageFont = Game.Fonts[$"{fontName};32"];
        confirmFont = Game.Fonts[$"{fontName};64"];

        WrapText();
        CreateUI();

        base.Initialize();
    }

    private void Exit()
    {
        foreach(var state in Game.ActiveStates)
        {
            state.Enable = true;
        }

        Game.RemoveGameState(this);
    }

    private void WrapText()
    {
        string current = "";
        float currentWidth = 0.0f;

        var tokens = message.Split(' ');
        foreach (var token in tokens)
        {
            float tokenWidth = messageFont.MeasureString(token + ' ').X;

            currentWidth += tokenWidth;
            current += token + ' ';

            if (currentWidth > lineWidth)
            {
                current += Environment.NewLine;
                currentWidth = 0.0f;
            }
        }

        message = current;
    }

    private void CreateUI()
    {
        Image image = new Image(new Sprite(Game.Textures[dialogTextureName], 0.8f))
        {
            Offset = Game.Resolution / 2.0f + new Vector2(0.0f, 40.0f),
        };
        UILayer.AddElement(image);

        Label label = new(new SpriteText(messageFont, message, color))
        {
            Offset = Game.Resolution / 2.0f,
        };
        UILayer.AddElement(label);

        Button button = new()
        {
            Offset = Game.Resolution / 2.0f + new Vector2(0.0f, label.Size.Y / 2.0f + 50.0f),
        };
        button.Label.SpriteText = new SpriteText(confirmFont, confirmText, color);
        button.HoverColor = Color.Yellow;
        button.OnClick += (sender, e) =>
        {
            Exit();
        };
        UILayer.AddElement(button);
    }
}
