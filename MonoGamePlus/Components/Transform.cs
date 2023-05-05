using Microsoft.Xna.Framework;

namespace MonoGamePlus.Components;
public struct Transform
{
    public Vector2 Position;
    public float Scale;
    public float Rotation;

    public Transform(Vector2 position)
    {
        Scale = 1.0f;
        Position = position;
    }

    public Transform()
        : this(Vector2.Zero) { }
}
