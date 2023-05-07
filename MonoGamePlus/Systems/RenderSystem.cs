using Arch.Core;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGamePlus.Components;
using MonoGamePlus.Resources;

namespace MonoGamePlus.Systems;
/// <summary>
/// Handles rendering of entities. Entities are divided into layer. To assign entity to some layer add
/// corresponding tag component to it. If entity does not have any render layer tag component then it
/// automatically belongs to normal layer. Layers are rendered in following order:
/// 1. background layer,
/// 2. normal layer,
/// 3. foreground layer,
/// 4. static layer,
/// Static layer does not use camera and clipping.
/// </summary>
public class RenderSystem : GameSystem
{
    private readonly QueryDescription backgroundLayer;
    private readonly QueryDescription normalLayer;
    private readonly QueryDescription foregroundLayer;
    private readonly QueryDescription staticLayer;

    public RenderSystem()
    {
        backgroundLayer = new QueryDescription().WithAll<Transform, Appearance, Background>();
        normalLayer = new QueryDescription().WithAll<Transform, Appearance>()
            .WithNone<Background, Foreground, Static>();
        foregroundLayer = new QueryDescription().WithAll<Transform, Appearance, Foreground>();
        staticLayer = new QueryDescription().WithAll<Transform, Appearance, Static>();
    }

    protected override void Update(float elapsed)
    {
        DrawLayer(backgroundLayer);
        DrawLayer(normalLayer);
        DrawLayer(foregroundLayer);
        DrawLayer(staticLayer, false, false);

        base.Update(elapsed);
    }

    private void DrawLayer(in QueryDescription query, bool useCamera = true, bool clipping = true)
    {
        if (useCamera)
        {
            Game.SpriteBatch.Begin(
                transformMatrix: GameState.Camera.GetTransformMatrix());
        }
        else
        {
            Game.SpriteBatch.Begin();
        }

        ECSWorld.Query(in query, (ref Transform transform, ref Appearance appearance) =>
        {
            foreach (var sprite in appearance.Sprites)
            {
                DrawSprite(ref transform, sprite, clipping);
            }
        });

        Game.SpriteBatch.End();
    }

    private void DrawSprite(ref Transform transform, Sprite sprite, bool clipping = true)
    {
        if (clipping)
        {
            float maxDistance = Game.Resolution.X + sprite.GetSize().Length();
            float distance = Vector2.Distance(
                GameState.Camera.Position,
                sprite.Position + transform.Position);

            if (distance >= maxDistance)
            {
                return;
            }
        }

        Game.SpriteBatch.Draw(sprite, transform.Position, transform.Scale, transform.Rotation);
    }
}
