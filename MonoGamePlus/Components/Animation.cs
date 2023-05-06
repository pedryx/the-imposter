using MonoGamePlus.Resources;

using System.Collections.Generic;

namespace MonoGamePlus.Components;
public struct Animation
{
    public AnimationFrame[] Frames;
    /// <summary>
    /// Time elapsed from last frame switch in seconds.
    /// </summary>
    public float Elapsed;
    /// <summary>
    /// Time spend at one frame in seconds.
    /// </summary>
    public float TimePerFrame;
    /// <summary>
    /// index of current frame.
    /// </summary>
    public int FrameIndex;
    public int StartIndex;

    public Animation(AnimationFrame[] frames, float timePerFrame = 0.1f)
    {
        Frames = frames;
        TimePerFrame = timePerFrame;
    }
}