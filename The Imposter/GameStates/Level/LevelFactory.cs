using Arch.Core;
using Arch.Core.Extensions;

using Microsoft.Xna.Framework;

using MonoGamePlus;
using MonoGamePlus.Components;
using MonoGamePlus.Resources;

using System.Collections.Generic;

namespace TheImposter.GameStates.Level;
internal class LevelFactory
{
    private readonly MGPGame game;
    private readonly World ecsWorld;
    private readonly Animations animations;

    public LevelFactory(GameState gameState)
    {
        game = gameState.Game;
        ecsWorld = gameState.ECSWorld;
        animations = new Animations();
    }

    private Sprite CreateCharacterSprite(Color color, string name)
        => new(game.Textures[name])
        {
            SourceRectangle = new Rectangle(0, 128, 64, 64),
            Origin = new Vector2(32, 38),
            Color = color,
        };

    private Sprite[] CreateCharacterSprites(Color color, bool clothes)
    {
        const float glovesChance = 0.33f;
        
        List<Sprite> sprites = new() { CreateCharacterSprite(color, "BODY_male") };

        if (clothes)
        {
            #region Random Clothes
            int feetIndex = game.Random.Next(3);
            if (feetIndex == 1)
                sprites.Add(CreateCharacterSprite(color, "FEET_plate_armor_shoes"));
            else if (feetIndex == 2)
                sprites.Add(CreateCharacterSprite(color, "FEET_shoes_brown"));

            int legsIndex = game.Random.Next(4);
            if (legsIndex == 1)
                sprites.Add(CreateCharacterSprite(color, "LEGS_pants_greenish"));
            else if (legsIndex == 2)
                sprites.Add(CreateCharacterSprite(color, "LEGS_plate_armor_pants"));
            else if (legsIndex == 3)
                sprites.Add(CreateCharacterSprite(color, "LEGS_robe_skirt"));

            int torsoIndex = game.Random.Next(10);
            if (torsoIndex == 1)
                sprites.Add(CreateCharacterSprite(color, "TORSO_chain_armor_jacket_purple"));
            else if (torsoIndex == 2)
                sprites.Add(CreateCharacterSprite(color, "TORSO_chain_armor_torso"));
            else if (torsoIndex == 3)
                sprites.Add(CreateCharacterSprite(color, "TORSO_leather_armor_bracers"));
            else if (torsoIndex == 4)
                sprites.Add(CreateCharacterSprite(color, "TORSO_leather_armor_shirt_white"));
            else if (torsoIndex == 5)
                sprites.Add(CreateCharacterSprite(color, "TORSO_leather_armor_shoulders"));
            else if (torsoIndex == 6)
                sprites.Add(CreateCharacterSprite(color, "TORSO_leather_armor_torso"));
            else if (torsoIndex == 7)
                sprites.Add(CreateCharacterSprite(color, "TORSO_plate_armor_arms_shoulders"));
            else if (torsoIndex == 8)
                sprites.Add(CreateCharacterSprite(color, "TORSO_plate_armor_torso"));
            else if (torsoIndex == 9)
                sprites.Add(CreateCharacterSprite(color, "TORSO_robe_shirt_brown"));

            int beltIndex = game.Random.Next(3);
            if (beltIndex == 1)
                sprites.Add(CreateCharacterSprite(color, "BELT_leather"));
            else if (beltIndex == 2)
                sprites.Add(CreateCharacterSprite(color, "BELT_rope"));

            int headIndex = game.Random.Next(7);
            if (headIndex == 1)
                sprites.Add(CreateCharacterSprite(color, "HEAD_hair_blonde"));
            else if (headIndex == 2)
                sprites.Add(CreateCharacterSprite(color, "HEAD_chain_armor_helmet"));
            else if (headIndex == 3)
                sprites.Add(CreateCharacterSprite(color, "HEAD_chain_armor_hood"));
            else if (headIndex == 4)
                sprites.Add(CreateCharacterSprite(color, "HEAD_leather_armor_hat"));
            else if (headIndex == 5)
                sprites.Add(CreateCharacterSprite(color, "HEAD_plate_armor_helmet"));
            else if (headIndex == 6)
                sprites.Add(CreateCharacterSprite(color, "HEAD_robe_hood"));

            if (game.Random.NextSingle() < glovesChance)
                sprites.Add(CreateCharacterSprite(color, "HANDS_plate_armor_gloves"));
            #endregion
        }

        return sprites.ToArray();
    }

    public Entity CreateFloorTile(Vector2 position)
        => ecsWorld.Create(
            new Transform(position),
            new Appearance(new Sprite(game.Textures["castle tiles"])
            {
                SourceRectangle = new Rectangle(160, 0, 32, 32),
                Origin = new Vector2(16),
            }),
            new Background());

    private Entity CreateCharacter(Vector2 position, Color color, bool clothes = true)
        => ecsWorld.Create(
            new Transform(position),
            new Appearance()
            {
                Sprites = CreateCharacterSprites(color, clothes),
            },
            new Movement(),
            new Animation(animations.WalkDown, 0.125f)
            {
                StartIndex = 1,
            });

    public Entity CreateNPC(Vector2 position, Color color)
    {
        var npc = CreateCharacter(position, Color.White);

        npc.Add(new PathFollow());
        npc.Get<Movement>().Speed = 150.0f;

        return npc;
    }

    public Entity CreatePlayer(Vector2 position)
    {
        var player = CreateCharacter(position, Color.White);

        player.Add(new Collider(new Vector2(30, 42))
        {
            Layer = (uint)CollisionLayers.Player,
            CollisionLayer = (uint)CollisionLayers.Walls,
        });
        player.Add<Foreground>();

        return player;
    }

    public Entity CreateImposter(Vector2 position)
    {
        var imposter = CreateNPC(position, new Color(80, 80, 80));

        imposter.Get<Appearance>().Sprites[0].Texture = game.Textures["BODY_skeleton"];
        imposter.Add<Foreground>();

        return imposter;
    }

    public Entity CreateWall(Vector2 start, Vector2 end, float width)
    {
        Vector2 size = start.X == end.X ? new Vector2(width, end.Y - start.Y + width)
            : new Vector2(end.X - start.X + width, width);

        return ecsWorld.Create(
            new Transform(start - new Vector2(width) / 2.0f + size / 2.0f),
            new Appearance(new Sprite(game.Textures.CreateRectangle(size, Color.Black))),
            new Collider(size)
            {
                Layer = (uint)CollisionLayers.Walls
            });
    }
}
