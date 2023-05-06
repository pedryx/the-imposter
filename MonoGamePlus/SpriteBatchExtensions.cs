using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGamePlus.Resources;

namespace MonoGamePlus;

/// <summary>
/// Contains extension methods for <see cref="SpriteBatch"/>.
/// </summary>
public static class SpriteBatchExtensions
{
    /// <summary>
    /// Render sprite with specific offset.
    /// </summary>
    /// <param name="sprite">Sprite to render.</param>
    /// <param name="positionOffset">Specific offset.</param>
    public static void Draw(
        this SpriteBatch spriteBatch,
        Sprite sprite,
        Vector2 positionOffset,
        float scaleOffset = 1.0f,
        float rotationOffset = 0.0f)
    {
        spriteBatch.Draw(
            sprite.Texture,
            sprite.Position + positionOffset,
            sprite.SourceRectangle,
            sprite.Color,
            sprite.Rotation + rotationOffset,
            sprite.Origin,
            sprite.Scale * scaleOffset,
            sprite.Effects,
            sprite.LayerDepth
        );
    }

    public static void Draw(
        this SpriteBatch spriteBatch,
        SpriteText spriteText,
        Vector2 positionOffset = default,
        float scaleOffset = 1.0f,
        float rotationOffset = 0.0f)
    {
        spriteBatch.DrawString(
            spriteText.Font,
            spriteText.Text,
            spriteText.Position + positionOffset,
            spriteText.Color,
            spriteText.Rotation + rotationOffset,
            spriteText.Origin,
            spriteText.Scale * scaleOffset,
            spriteText.SpriteEffects,
            spriteText.LayerDepth
        );
    }

    /// <summary>
    /// Render sprite.
    /// </summary>
    /// <param name="sprite">Sprite to render.</param>
    public static void Draw(this SpriteBatch spriteBatch, Sprite sprite)
        => Draw(spriteBatch, sprite, Vector2.Zero);
}
