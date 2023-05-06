using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace MonoGamePlus.Resources;
public class AnimationFactory
{
    //private readonly TextureManager textures;

    /*public AnimationFactory(TextureManager textures)
    {
        this.textures = textures;
    }*/

    /*public List<AnimationFrame> CreateImagesAnimation(List<string> images)
    {
        var frames = new List<AnimationFrame>();
        foreach (var image in images)
        {
            frames.Add(new AnimationFrame()
            {
                Texture = textures[image],
            });
        }

        return frames;
    }

    public List<AnimationFrame> CreateSpriteSheetAnimation(string image, List<Rectangle> sources)
    {
        var frames = new List<AnimationFrame>();
        foreach (var source in sources)
        {
            frames.Add(new AnimationFrame()
            {
                Texture = textures[image],
                SourceRectangle = source,
            });
        }

        return frames;
    }*/

    public static AnimationFrame[] CreateSpriteSheetAnimationX(/*string image, */int y, int start, int count, Vector2 size)
    {
        var frames = new List<AnimationFrame>();
        for (int x = 0; x < count; x++)
        {
            frames.Add(new AnimationFrame()
            {
                //Texture = textures[image],
                SourceRectangle = new Rectangle()
                {
                    X = (int)(start + size.X * x),
                    Y = (int)(y * size.Y),
                    Size = size.ToPoint(),
                }
            });
        }

        return frames.ToArray();
    }

    /*public List<AnimationFrame> CreateSpriteSheetAnimationY(string image, int x, int start, int count, Vector2 size)
    {
        var frames = new List<AnimationFrame>();
        for (int y = 0; y < count; y++)
        {
            frames.Add(new AnimationFrame()
            {
                Texture = textures[image],
                SourceRectangle = new Rectangle()
                {
                    X = (int)(x * size.X),
                    Y = (int)(start + size.Y * y),
                    Size = size.ToPoint(),
                }
            });
        }

        return frames;
    }*/
}
