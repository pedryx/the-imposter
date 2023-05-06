using Arch.Core;

namespace MonoGamePlus.Events.Events;
public delegate void EntityEventHandler(object sender, EntityEventArgs e);

public class EntityEventArgs
{
    public Entity Entity;

    public EntityEventArgs(Entity entity)
    {
        Entity = entity;
    }
}
