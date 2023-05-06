using Arch.Core;

using MonoGamePlus.Components;

namespace MonoGamePlus.Systems;
public class AnimationSystem : GameSystem<Appearance, Animation>
{
    protected override void Update(
        float elapsed,
        in Entity entity,
        ref Appearance appearance,
        ref Animation animation)
    {
        animation.Elapsed += elapsed * GameState.Game.Speed;
        if (animation.Elapsed >= animation.TimePerFrame)
        {
            animation.Elapsed -= animation.TimePerFrame;

            //appearance.Sprites.Texture = animation.Frames[animation.FrameIndex].Texture;
            //appearance.Sprites.SourceRectangle = animation.Frames[animation.FrameIndex].SourceRectangle;

            animation.FrameIndex++;
            if (animation.FrameIndex >= animation.Frames.Count)
                animation.FrameIndex = 0;
        }

        base.Update(elapsed, entity, ref appearance, ref animation);
    }
}
