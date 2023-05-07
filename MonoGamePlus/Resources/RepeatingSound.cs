using Microsoft.Xna.Framework.Audio;

using System;

namespace MonoGamePlus.Resources;
public class RepeatingSound
{
    private readonly Random random = new();
    private readonly SoundEffect effect;

    private DateTime lastPlayed = DateTime.Now;

    public RepeatingSound(SoundEffect effect)
    {
        this.effect = effect;
    }

    public void Play(float volume, float pause)
    {
        TimeSpan elapsed = DateTime.Now - lastPlayed;
        if (elapsed > effect.Duration + TimeSpan.FromSeconds(pause))
        {
            effect.Play(volume, random.NextSingle(), 0.0f);
            lastPlayed = DateTime.Now;
        }
    }
}
