using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePlus.Resources;
/// <summary>
/// Sprite is texture together with information how should texture be rendered.
/// </summary>
public struct Sprite
{
    public Texture2D Texture;
    public Vector2 Position;
    public Rectangle? SourceRectangle;
    public Color Color;
    public float Rotation;
    public Vector2 Origin;
    public Vector2 Scale;
    public SpriteEffects Effects;
    public float LayerDepth;

    public Vector2 GetSize()
    {
        if (Texture == null)
            return Vector2.Zero;

        Vector2 size = SourceRectangle.HasValue ? SourceRectangle.Value.Size.ToVector2() : Texture.GetSize();
        return size * Scale;
    }

    public Sprite(Vector2 scale)
    {
        Scale = scale;
        Color = Color.White;
    }

    public Sprite(float scale)
        : this(new Vector2(scale)) { }

    public Sprite()
        : this(Vector2.One) { }

    public Sprite(Texture2D texture, Vector2 scale)
        : this(scale)
    {
        Texture = texture;
        Origin = texture.GetSize() / 2.0f;
    }

    public Sprite(Texture2D texture, float scale = 1.0f)
        : this(texture, new Vector2(scale)) { }

    public Sprite(Texture2D texture)
        : this(texture, Vector2.One) { }
}
