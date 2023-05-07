using Microsoft.Xna.Framework.Audio;

using MonoGamePlus;

namespace TheImposter;
internal static class LevelMusic
{
    private static bool first = true;
    private static SoundEffectInstance clock;

    public static void Play(MGPGame game)
    {
        if (first)
        {
            first = true;
            clock = game.Sounds["clock"].CreateInstance();
            clock.IsLooped = true;
            clock.Volume = 0.3f;
            clock.Play();
            return;
        }

        clock.Play();
    }

    public static void Resume()
    {
        clock?.Resume();
    }

    public static void Puase()
    {
        clock?.Pause();
    }

    public static void Stop()
    {
        clock?.Stop();
        clock = null;
    }
}
