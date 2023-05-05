using Arch.Core;

namespace MonoGamePlus.Events;
public delegate void EntityEventhandler(object sender, EntityEventArgs e);

public class EntityEventArgs
{
    public Entity Entity;

    public EntityEventArgs(Entity entity)
    {
        Entity = entity;
    }
}
