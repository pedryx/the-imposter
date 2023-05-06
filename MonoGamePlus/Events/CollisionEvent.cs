using Arch.Core;

namespace MonoGamePlus.Events.Events;
public delegate void CollisionEventHandler(object sender, CollisionEventArgs e);

public class CollisionEventArgs
{
    public Entity Self;
    public Entity Target;

    public CollisionEventArgs(Entity self, Entity target)
    {
        Self = self;
        Target = target;
    }
}
