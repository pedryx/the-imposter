using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGamePlus.Systems;
public class CameraMoveControlSystem : GameSystem
{
    public float MoveSpeed { get; set; } = 1000.0f;
    public Keys UpKey { get; set; } = Keys.W;
    public Keys LeftKey { get; set; } = Keys.A;
    public Keys DownKey { get; set; } = Keys.S;
    public Keys RightKey { get; set; } = Keys.D;
    public Rectangle Border { get; set; }


    public CameraMoveControlSystem(Vector2 worldSize)
    {
        Border = new Rectangle()
        {
            Location = (-worldSize / 2.0f).ToPoint(),
            Size = worldSize.ToPoint()
        };
    }

    protected override void Update(float elapsed)
    {
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

        if (movementDirection != Vector2.Zero)
            movementDirection.Normalize();

        GameState.Camera.Position += movementDirection * MoveSpeed * elapsed * (1 / GameState.Camera.Scale);

        base.Update(elapsed);
    }
}
