using Microsoft.Xna.Framework;

using MonoGamePlus.Resources;

namespace MonoGamePlus.UI.Elements;
/// <summary>
/// Represent simple label UI element.
/// </summary>
public class Label : UIElement
{
    public SpriteText SpriteText;

    public override Vector2 Size => SpriteText.GetSize();

    public Label(SpriteText spriteText = default)
    {
        SpriteText = spriteText;
    }

    public override void Draw(float elapsed, Vector2 position)
    {
        if (SpriteText.Font != null)
            Game.SpriteBatch.Draw(SpriteText, position);

        base.Draw(elapsed, position);
    }
}
