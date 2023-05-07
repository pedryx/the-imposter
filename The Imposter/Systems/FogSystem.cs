using Arch.Core;
using Arch.Core.Extensions;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGamePlus;
using MonoGamePlus.Components;
using MonoGamePlus.Resources;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TheImposter.Components;
using TheImposter.GameStates.Level;

namespace TheImposter.Systems;
internal class FogSystem : GameSystem<Transform, Movement, FogCloud>
{
    private const int cloudsCount = 300;
    private const int border = 512;

    private readonly LevelFactory factory;
    private readonly Entity target;

    public FogSystem(LevelFactory factory, Entity target)
    {
        this.factory = factory;
        this.target = target;
    }

    protected override void Initialize()
    {
        for (int i = 0; i < cloudsCount; i++)
        {
            factory.CreateFogCloud(Game.Random.NextVector2(Game.Resolution + 2.0f * new Vector2(border))
                - new Vector2(border));
        }

        base.Initialize();
    }

    protected override void Update(
        float elapsed,
        in Entity entity,
        ref Transform transform,
        ref Movement movement,
        ref FogCloud cloud)
    {
        movement.Speed = cloud.Speed;// + target.Get<Movement>().Speed;

        if (transform.Position.X < -border)
        {
            ECSWorld.Destroy(entity);

            factory.CreateFogCloud(new Vector2()
            {
                X = Game.Resolution.X + border,
                Y = Game.Random.NextSingle(-border, GameState.Game.Resolution.Y + border),
            });
        }

        base.Update(elapsed, entity, ref transform, ref movement, ref cloud);
    }
}
