using MonoGamePlus.Resources;

using System.Collections.Generic;

namespace TheImposter;
internal class SoundSequences
{
    public SoundSequence Walking { get; private set; }

    public SoundSequences(SoundManager sounds)
    {
        Walking = new SoundSequence(sounds, new List<string>()
        {
            "0", "1", "2", "3", "4", "5", "6", "7", "8"
        }, 0.3f);
    }
}
