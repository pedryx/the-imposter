using Arch.Core;

namespace MonoGamePlus.Events;
public delegate void EntityUpdateEventHandler(object sender, EntityUpdateEventArgs e);

public class EntityUpdateEventArgs
{
    public Entity Entity { get; private set; }
    public float Elapsed { get; private set; }

    public EntityUpdateEventArgs(Entity entity, float elapsed)
    {
        Entity = entity;
        Elapsed = elapsed;
    }
}
