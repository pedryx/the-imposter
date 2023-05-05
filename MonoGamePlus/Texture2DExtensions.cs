using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePlus;

/// <summary>
/// Contains extension methods for <see cref="Texture2D"/>.
/// </summary>
public static class Texture2DExtensions
{
    /// <summary>
    /// Get size of the texture.
    /// </summary>
    public static Vector2 GetSize(this Texture2D texture)
        => new(texture.Width, texture.Height);
}
