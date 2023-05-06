using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace MonoGamePlus.UI.Elements;
public class StackPanel : UIElement
{
    private float padding = 5.0f;
    private bool vertical = true;
    private readonly List<UIElement> elements = new();

    public float Padding
    {
        get => padding;
        set
        {
            padding = value;
            Reposition();
        }
    }

    public bool Vertical
    {
        get => vertical;
        set
        {
            vertical = value;
            Reposition();
        }
    }

    public override Vector2 Size
    {
        get
        {
            float maxWidth = 0.0f;
            float sumHeight = Padding;

            foreach (UIElement element in elements)
            {
                Vector2 size = element.Size;

                if (size.X > maxWidth)
                    maxWidth = size.X;

                sumHeight += size.Y;
                sumHeight += Padding;
            }

            return new Vector2(maxWidth, sumHeight);
        }
    }

    public StackPanel(Vector2 position = default)
    {
        Offset = position;
    }

    public void Add(UIElement element)
    {
        elements.Add(element);
        AddChild(element);

        Reposition();
    }

    private void Reposition()
    {
        Vector2 size = Size;

        if (Vertical)
        {
            float y = -size.Y / 2.0f + Padding;
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].Offset = new Vector2(0.0f, y + elements[i].Size.Y / 2.0f);
                y += elements[i].Size.Y;
                y += Padding;
            }
        }
        else
        {
            float x = -size.X / 2.0f + Padding;
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].Offset = new Vector2(x + elements[i].Size.X / 2.0f, 0.0f);
                x += elements[i].Size.X;
                x += Padding;
            }
        }
    }
}
