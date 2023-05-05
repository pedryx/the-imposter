﻿using Microsoft.Xna.Framework.Graphics;

using SpriteFontPlus;

using System.IO;
using System.Linq;

namespace MonoGamePlus.Resources;
public class FontManager : ResourceManager<SpriteFont>
{
    private const int bitmapSize = 1024;

    public FontManager(MGPGame game)
        : base(game, "Fonts") { }

    public override SpriteFont Load(string path, string name)
    {
        int size = int.Parse(name.Split(';').Last());
        SpriteFont font = TtfFontBaker.Bake(
            File.ReadAllBytes(path),
            size,
            bitmapSize,
            bitmapSize,
            new[] { CharacterRange.BasicLatin }
        ).CreateSpriteFont(Game.GraphicsDevice);

        return font;
    }
}
