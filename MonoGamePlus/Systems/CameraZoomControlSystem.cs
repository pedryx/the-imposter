using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGamePlus.Systems;
public class CameraZoomControlSystem : GameSystem
{
    private MouseState lastMouseState;

    public float ZoomSpeed { get; set; } = 1.0f;
    public float MinZoom { get; set; } = 0.5f;
    public float MaxZoom { get; set; } = 2.0f;

    protected override void Initialize()
    {
        base.Initialize();

        GameState.Camera.Scale = 0.15f;// (MaxZoom - MinZoom) / 2.0f + MinZoom;
        lastMouseState = Mouse.GetState();
    }

    protected override void Update(float elapsed)
    {
        MouseState mouseState = Mouse.GetState();

        float zoomDirection = 0.0f;
        if (mouseState.ScrollWheelValue > lastMouseState.ScrollWheelValue)
            zoomDirection = 1.0f;
        else if (mouseState.ScrollWheelValue < lastMouseState.ScrollWheelValue)
            zoomDirection = -1.0f;

        //GameState.Camera.Scale *= (1.0f + zoomDirection * ZoomSpeed * elapsed);
        //GameState.Camera.Scale = MathHelper.Clamp(GameState.Camera.Scale, MinZoom, MaxZoom);

        lastMouseState = mouseState;

        base.Update(elapsed);
    }
}
