using Arch.Core;

using Microsoft.Xna.Framework;

using MonoGamePlus;
using MonoGamePlus.Components;
using MonoGamePlus.Resources;
using MonoGamePlus.Systems;

namespace TheImposter.GameStates;
internal class LevelState : GameState
{
    public Entity Player { get; private set; }

    protected override void Initialize()
    {
        CreateEntities();
        CreateSystems();

        Camera.Target = Player;

        base.Initialize();
    }

    public void CreateSystems()
    {
        AddUpdateSystem(new CameraZoomControlSystem());
        AddUpdateSystem(new EntityControlSystem(Player));

        AddUpdateSystem(new MovementSystem());
        AddUpdateSystem(new CollisionSystem());

        AddRenderSystem(new RenderSystem());
    }

    public void CreateEntities()
    {
        Player = ECSWorld.Create(
            new Transform(Vector2.Zero),
            new Appearance(new Sprite(Game.Textures.CreateCircle(30, Color.Pink))),
            new Movement(),
            new Collider(new Vector2(60, 60))
            {
                Layer = (uint)CollisionLayers.Player,
                CollisionLayer = (uint)CollisionLayers.Walls,
            });

        ECSWorld.Create(
            new Transform(new Vector2(-200.0f, -200.0f)),
            new Appearance(new Sprite(Game.Textures.CreateRectangle(new Vector2(300, 300), Color.Blue))),
            new Collider(new Vector2(300, 300))
            {
                Layer = (uint)CollisionLayers.Walls,
            });

        ECSWorld.Create(
            new Transform(new Vector2(200.0f, 200.0f)),
            new Appearance(new Sprite(Game.Textures.CreateCircle(100, Color.Red))),
            new Collider(new Vector2(200, 200))
            {
                Layer = (uint)CollisionLayers.Walls,
            });
    }
}
