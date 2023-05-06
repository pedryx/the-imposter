using Arch.Core;

using MonoGamePlus;
using MonoGamePlus.Components;

using System;

namespace TheImposter.Systems;
internal class AnimationOrientationSystem : GameSystem<Animation, Movement>
{
    private readonly Animations animations = new();

    protected override void Update(float elapsed, in Entity entity, ref Animation animation, ref Movement movement)
    {
        while (movement.Direction > 2.0f * MathF.PI)
            movement.Direction -= 2.0f * MathF.PI;
        while (movement.Direction < 0.0f)
            movement.Direction += 2.0f * MathF.PI;

        if (movement.Speed == 0.0f)
            animation.Frames = animations.WalkDown;
        else if (movement.Direction > 1.0f * MathF.PI / 4.0f && movement.Direction <= 3.0f * MathF.PI / 4.0f)
            animation.Frames = animations.WalkDown;
        else if (movement.Direction > 3.0f * MathF.PI / 4.0f && movement.Direction <= 5.0f * MathF.PI / 4.0f)
            animation.Frames = animations.WalkLeft;
        else if (movement.Direction > 5.0f * MathF.PI / 4.0f && movement.Direction <= 7.0f * MathF.PI / 4.0f)
            animation.Frames = animations.WalkUp;
        else
            animation.Frames = animations.WalkRight;

        base.Update(elapsed, entity, ref animation, ref movement);
    }
}
