using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePlus.Resources;
public struct SpriteText
{
    public SpriteFont Font;
    public string Text = "";
    public Vector2 Position;
    public Color Color = Color.White;
    public float Rotation;
    public Vector2 Origin;
    public float Scale = 1.0f;
    public SpriteEffects SpriteEffects;
    public float LayerDepth;

    public Vector2 GetSize()
    {
        if (Font == null)
            return Vector2.Zero;

        return Font.MeasureString(Text);
    }

    public SpriteText(SpriteFont font, string text, Color color)
    {
        Font = font;
        Text = text;
        Color = color;

        Origin = font.MeasureString(text) / 2.0f;
    }

    public SpriteText(SpriteFont font, string text = "")
        : this(font, text, Color.White) { }
}
