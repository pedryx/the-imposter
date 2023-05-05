using Arch.Core;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGamePlus.Components;

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
                transformMatrix: GameState.Camera.GetTransformMatrix(),
                sortMode: SpriteSortMode.FrontToBack);
        }
        else
        {
            Game.SpriteBatch.Begin(
                sortMode: SpriteSortMode.FrontToBack);
        }

        ECSWorld.Query(in query, (ref Transform transform, ref Appearance appearance) =>
        {
            DrawEntity(ref transform, ref appearance, clipping);
        });

        Game.SpriteBatch.End();
    }

    private void DrawEntity(ref Transform transform, ref Appearance appearance, bool clipping = true)
    {
        if (clipping)
        {
            float maxDistance = appearance.Sprite.GetSize().Length();
            float distance = Vector2.Distance(
                GameState.Camera.Position,
                appearance.Sprite.Position + transform.Position);

            if (distance >= maxDistance)
            {
                return;
            }
        }

        Game.SpriteBatch.Draw(appearance.Sprite, transform.Position);
    }
}
