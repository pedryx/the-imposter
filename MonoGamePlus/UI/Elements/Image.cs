using Microsoft.Xna.Framework;

using MonoGamePlus.Resources;

namespace MonoGamePlus.UI.Elements;
/// <summary>
/// Represent simple image UI element.
/// </summary>
public class Image : UIElement
{
    public Sprite Sprite;

    public override Vector2 Size => Sprite.GetSize();

    public Image(Sprite sprite = default)
    {
        Sprite = sprite;
    }

    public override void Draw(float elapsed, Vector2 position)
    {
        if (Sprite.Texture != null)
            Game.SpriteBatch.Draw(Sprite, position);

        base.Draw(elapsed, position);
    }
}
