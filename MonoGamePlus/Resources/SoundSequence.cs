using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

using System;
using System.Collections.Generic;

namespace MonoGamePlus.Resources;
/// <summary>
/// Represent sequence with sounds.
/// </summary>
public class SoundSequence
{
    private readonly List<SoundEffect> sounds = new();
    /// <summary>
    /// How many seconds to pause between sounds.
    /// </summary>
    private readonly float pause;

    /// <summary>
    /// Determine if this is first play of the sound.
    /// </summary>
    private bool first = true;
    /// <summary>
    /// Timestamp of last play of the sound.
    /// </summary>
    private DateTime lastPlayed;
    /// <summary>
    /// Index of current sound.
    /// </summary>
    private int current = 0;

    private SoundSequence(List<SoundEffect> sounds, float pause)
    {
        this.sounds = sounds;
        this.pause = pause;
        first = true;
    }

    /// <param name="pause">How many seconds to pause between sounds.</param>
    public SoundSequence(SoundManager manager, List<string> soundNames, float pause)
    {
        this.pause = pause;

        foreach (var soundName in soundNames)
        {
            sounds.Add(manager[soundName]);
        }
    }

    /// <summary>
    /// Continue playing sequence.
    /// </summary>
    public void Play(float volume, Vector2 position)
    {
        if (first)
        {
            first = false;
            lastPlayed = DateTime.Now;
            //sounds[current].Play(volume, 0.0f, 0.0f);
            PlaySound(volume, position);
        }

        if (DateTime.Now - lastPlayed > sounds[current].Duration + TimeSpan.FromSeconds(pause))
        {
            current++;
            if (current == sounds.Count)
                current = 0;

            //sounds[current].Play(volume, 0.0f, 0.0f);
            PlaySound(volume, position);
            lastPlayed = DateTime.Now;
        }
    }

    private void PlaySound(float volume, Vector2 position)
    {
        var instance = sounds[current].CreateInstance();
        instance.Volume = volume;

        instance.Apply3D(new AudioListener()
        {
            Forward = Vector3.UnitZ,
            Up = -Vector3.UnitY,
            Velocity = Vector3.Zero,
            Position = Vector3.Zero,
        },
        new AudioEmitter()
        {
            Forward = Vector3.UnitZ,
            Up = -Vector3.UnitY,
            Velocity = Vector3.Zero,
            Position = new Vector3(position, 0.0f),
            //DopplerScale = 100.0f,
        });

        instance.Play();
    }

    public SoundSequence Clone()
        => new(sounds, pause);
}