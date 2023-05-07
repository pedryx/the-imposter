using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace MonoGamePlus.Resources;
public class TextureManager : ResourceManager<Texture2D>
{
    public TextureManager(MGPGame game) 
        : base(game, "Textures") { }

    public override Texture2D Load(string path, string name)
    {
        Texture2D texture = Texture2D.FromFile(Game.GraphicsDevice, path);
        texture.Name = name;

        return texture;
    }

    public Texture2D CreateRectangle(Vector2 size, Color color)
    {
        Color[] pixels = new Color[(int)size.X * (int)size.Y];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }

        var texture = new Texture2D(Game.GraphicsDevice, (int)size.X, (int)size.Y);
        texture.SetData(pixels);

        return texture;
    }

    public Texture2D CreateRectangle(Vector2 size)
        => CreateRectangle(size, Color.White);

    public Texture2D CreateCircle(int radius, Color color)
    {
        int size = 2 * radius;
        Color[] pixels = new Color[size * size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if ((x - radius) * (x - radius) + (y - radius) * (y - radius) < radius * radius)
                    pixels[y * size + x] = color;
            }
        }

        var texture = new Texture2D(Game.GraphicsDevice, size, size);
        texture.SetData(pixels);

        return texture;
    }

    public Texture2D CreateCircle(int radius)
        => CreateCircle(radius, Color.White);

    public Texture2D CreateVisibilityCircle(Vector2 size, float radius)
    {
        Color[] pixels = new Color[(int)size.X * (int)size.Y];
        Vector2 center = size / 2.0f;

        for (int x = 0; x < size.X; x++)
        {
            for (int y = 0; y < size.Y; y++)
            {
                Vector2 position = new(x, y);
                float distance = Vector2.Distance(center, position);

                if (distance <= radius)
                {
                    float value = EaseInQuad(distance / radius);
                    pixels[y * (int)size.X + x] = Interlope(new Color(0, 0, 0, 0), new Color(0, 0, 0, 0.85f), value);
                }
                else
                    pixels[y * (int)size.X + x] = new Color(0, 0, 0, 0.85f);
            }
        }

        var texture = new Texture2D(Game.GraphicsDevice, (int)size.X, (int)size.Y);
        texture.SetData(pixels);

        return texture;
    }

    private Color Interlope(Color start, Color end, float value)
        => new Color(
            Interlope(start.R, end.R, value),
            Interlope(start.G, end.G, value),
            Interlope(start.B, end.B, value),
            Interlope(start.A, end.A, value));

    private byte Interlope(byte start, byte end, float value)
        => (byte)((end - start) * value + start);

    private float EaseInQuad(float x)
        => x * x;
}
