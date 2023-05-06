using Microsoft.Xna.Framework;

using MonoGamePlus.Resources;

using System.Collections.Generic;

namespace TheImposter;
internal class Animations
{
    public AnimationFrame[] IdleUp { get; protected set; }
    public AnimationFrame[] IdleLeft { get; protected set; }
    public AnimationFrame[] IdleDown { get; protected set; }
    public AnimationFrame[] IdleRight { get; protected set; }

    public AnimationFrame[] WalkUp { get; protected set; }
    public AnimationFrame[] WalkLeft { get; protected set; }
    public AnimationFrame[] WalkDown { get; protected set; }
    public AnimationFrame[] WalkRight { get; protected set; }

    public Animations()
    {
        Vector2 size = new(64);

        IdleUp = AnimationFactory.CreateSpriteSheetAnimationX(0, 0, 1, size);
        IdleLeft = AnimationFactory.CreateSpriteSheetAnimationX(1, 0, 1, size);
        IdleDown = AnimationFactory.CreateSpriteSheetAnimationX(3, 0, 1, size);
        IdleRight = AnimationFactory.CreateSpriteSheetAnimationX(2, 0, 1, size);

        WalkUp = AnimationFactory.CreateSpriteSheetAnimationX(0, 1, 9, size);
        WalkLeft = AnimationFactory.CreateSpriteSheetAnimationX(1, 1, 9, size);
        WalkDown = AnimationFactory.CreateSpriteSheetAnimationX(2, 1, 9, size);
        WalkRight = AnimationFactory.CreateSpriteSheetAnimationX(3, 1, 9, size);
    }
}
