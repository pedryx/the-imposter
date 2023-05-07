using Arch.Core;
using Arch.Core.Extensions;

using Microsoft.Xna.Framework;

using MonoGamePlus;
using MonoGamePlus.Components;

using TheImposter.Components;

namespace TheImposter.Systems;
internal class NoiseSystem : GameSystem<Transform, Noise>
{
    private readonly Entity target;
    private readonly float noiseDistance;

    private Vector2 targetPosition;

    public NoiseSystem(Entity target, float noiseDistance)
    {
        this.target = target;
        this.noiseDistance = noiseDistance;
    }

    protected override void PreUpdate(float elapsed)
    {
        targetPosition = target.Get<Transform>().Position;

        base.PreUpdate(elapsed);
    }

    protected override void Update(float elapsed, in Entity entity, ref Transform transform, ref Noise noise)
    {
        float distance = Vector2.Distance(targetPosition, transform.Position);

        if (noise.Ellapsed > 0.0f)
        {
            noise.Ellapsed -= elapsed;
        }

        if (distance < noiseDistance && noise.Ellapsed <= 0.0f)
        {
            noise.Ellapsed = noise.Cooldown;
            noise.Sound.Play(0.1f, 0.0f, 0.0f);
        }

        base.Update(elapsed, entity, ref transform, ref noise);
    }
}
