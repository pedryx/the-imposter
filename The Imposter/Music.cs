using Microsoft.Xna.Framework.Audio;

using MonoGamePlus;

namespace TheImposter;
internal static class Music
{
    private static bool first = true;

    public static void Play(MGPGame game)
    {
        if (!first)
            return;
        first = false;

        Play(game, "Is Anybody Home", 0.3f);
    }

    private static void Play(MGPGame game, string name, float volume)
    {
        SoundEffectInstance sound = game.Sounds[name].CreateInstance();
        sound.IsLooped = true;
        sound.Volume = volume;
        sound.Play();
    }
}
