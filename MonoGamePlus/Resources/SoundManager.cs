using Microsoft.Xna.Framework.Audio;

namespace MonoGamePlus.Resources;
public class SoundManager : ResourceManager<SoundEffect>
{
    public SoundManager(MGPGame game)
        : base(game, "Sounds") { }

    public override SoundEffect Load(string path, string name)
    {
        SoundEffect effect = SoundEffect.FromFile(path);
        effect.Name = name;

        return effect;
    }
}
