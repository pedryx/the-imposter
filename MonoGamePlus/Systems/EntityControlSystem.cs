using Arch.Core;
using Arch.Core.Extensions;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MonoGamePlus.Components;
using MonoGamePlus.Events;

namespace MonoGamePlus.Systems;
public class EntityControlSystem : GameSystem
{
    private bool lastMovement;
    private KeyboardState lastKeyboardState;

    public Entity Target { get; set; }

    public Keys UpKey { get; set; } = Keys.W;
    public Keys LeftKey { get; set; } = Keys.A;
    public Keys DownKey { get; set; } = Keys.S;
    public Keys RightKey { get; set; } = Keys.D;
    public float Speed { get; set; } = 500.0f;

    public event EntityEventhandler OnMoveStart;
    public event EntityEventhandler OnMoveEnd;

    public EntityControlSystem(Entity target)
    {
        Target = target;
        lastKeyboardState = Keyboard.GetState();
    }

    protected override void Update(float elapsed)
    {
        ref Movement movement = ref Target.Get<Movement>();

        KeyboardState keyboardState = Keyboard.GetState();

        Vector2 movementDirection = new();
        if (keyboardState.IsKeyDown(UpKey))
            movementDirection += -Vector2.UnitY;
        if (keyboardState.IsKeyDown(LeftKey))
            movementDirection += -Vector2.UnitX;
        if (keyboardState.IsKeyDown(DownKey))
            movementDirection += Vector2.UnitY;
        if (keyboardState.IsKeyDown(RightKey))
            movementDirection += Vector2.UnitX;
        bool currentMovement = movementDirection != Vector2.Zero;

        if (currentMovement)
            movement.Direction = MathUtils.VectorToAngle(movementDirection);

        if (currentMovement & !lastMovement)
        {
            movement.Speed = Speed;
            OnMoveStart?.Invoke(this, new EntityEventArgs(Target));
        }
        else if (!currentMovement & lastMovement)
        {
            movement.Speed = 0.0f;
            OnMoveEnd.Invoke(this, new EntityEventArgs(Target));
        }

        base.Update(elapsed);
    }
}
