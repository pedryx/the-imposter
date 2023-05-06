using Microsoft.Xna.Framework;

namespace MonoGamePlus.Components;
public struct Collider
{
    public Vector2 Size;
    public uint ReactionLayer;
    public uint CollisionLayer;
    public uint Layer;

    public Collider(Vector2 size)
    {
        Size = size;
    }
}
