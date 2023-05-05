using Arch.Core;

using MonoGamePlus.Components;

namespace MonoGamePlus.Systems;
public class MovementSystem : GameSystem<Transform, Movement>
{
    protected override void Update(float elapsed, in Entity entity, ref Transform transform, ref Movement movement)
    {
        transform.Position += MathUtils.AngleToVector(movement.Direction) * movement.Speed * elapsed * Game.Speed;

        base.Update(elapsed, entity, ref transform, ref movement);
    }
}
