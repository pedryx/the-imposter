using Arch.Core;

using Microsoft.Xna.Framework;

using MonoGamePlus;

using System.Collections.Generic;
using System.Linq;

namespace TheImposter.GameStates.Level;
internal class WorldGenerator
{
    private const float doorWidth = 96.0f;
    private const float houseWidth = 3072.0f;
    private const float houseHeight = 2048.0f;

    private const float outsideWallsWidth = 32.0f;
    private const float insideWallsWidth = 16.0f;

    private const int nodesPerRoom = 100;
    private const float characterSize = 64.0f;

    #region Mansion Constants
    private const float roomAWidth = houseWidth / 3.0f;
    private const float roomAHeight = houseHeight / 5.0f;
    private const float doorWallASmall = (roomAWidth - roomBWidth - doorWidth) / 2.0f;
    private const float doorWallABig = (roomAWidth - doorWidth) / 2.0f;

    private const float roomBWidth = houseWidth / 5.0f;
    private const float roomBHeight = houseHeight - 2.0f * roomAHeight;
    private const float doorWallB = (roomBHeight - roomCHeight - 2.0f * doorWidth) / 4.0f;

    private const float roomCWidth = (houseWidth - 2.0f * roomBWidth) / 2.0f;
    private const float roomCHeight = roomBHeight * 0.6f;
    private const float doorWallC = (roomCWidth - doorWidth) / 2.0f;

    private const float hallWidth = houseWidth - 2.0f * roomBWidth;
    private const float hallHeight = 2.0f * doorWallB + doorWidth;

    private const float doorOffsetASide = roomBWidth + doorWallASmall + doorWidth / 2.0f;
    #endregion

    private readonly LevelFactory factory;
    private readonly MGPGame game;

    public Graph Graph { get; private set; }
    public Entity[] Imposters { get; private set; }
    public Vector2 Spawn { get; private set; }

    public WorldGenerator(LevelFactory factory, GameState gameState)
    {
        this.factory = factory;
        game = gameState.Game;
    }

    public void Generate(
        int npcCount,
        int imposterCount,
        bool impostersClothes,
        bool imposterSkeleton,
        bool imposterMovement,
        bool imposterAnimation,
        bool imposterNoise)
    {
        Graph = new Graph();
        Spawn = new Vector2(houseWidth / 2.0f - roomBWidth / 2.0f, 0.0f);

        CreateFloor();
        CreateMansion();
        SpawnNPCs(npcCount);
        SpawnImposters(
            imposterCount,
            impostersClothes,
            imposterSkeleton,
            imposterMovement,
            imposterAnimation,
            imposterNoise);
    }

    private void SpawnImposters(int count, bool clothes, bool skeleton, bool movement, bool animation, bool noise)
    {
        Imposters = new Entity[count];
        for (int i = 0; i < count; i++)
        {
            Imposters[i] = factory.CreateImposter(
                Graph.GetRandomNode(game.Random).ToVector2(),
                clothes,
                skeleton,
                movement,
                animation,
                noise);
        }
    }

    private void SpawnNPCs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            factory.CreateNPC(Graph.GetRandomNode(game.Random).ToVector2(), Color.White);
        }
    }

    private void CreateMansion()
    {
        #region Mansion walls Definitions
        Vector2 topLeft = new Vector2(-houseWidth, -houseHeight) / 2.0f;

        // border
        // top
        factory.CreateWall(
            topLeft,
            topLeft + new Vector2(houseWidth, 0.0f),
            outsideWallsWidth);
        // bottom
        factory.CreateWall(
            topLeft + new Vector2(0.0f, houseHeight),
            topLeft + new Vector2(houseWidth, houseHeight),
            outsideWallsWidth);
        // left
        factory.CreateWall(
            topLeft,
            topLeft + new Vector2(0.0f, houseHeight),
            outsideWallsWidth);
        // right
        factory.CreateWall(
            topLeft + new Vector2(houseWidth, 0.0f),
            topLeft + new Vector2(houseWidth, houseHeight),
            outsideWallsWidth);

        // A rooms
        factory.CreateWall(
            topLeft + new Vector2(0.0f, roomAHeight),
            topLeft + new Vector2(roomBWidth + doorWallASmall, roomAHeight),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(roomBWidth + doorWallASmall + doorWidth, roomAHeight),
            topLeft + new Vector2(roomAWidth + doorWallABig, roomAHeight),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(roomAWidth + doorWallABig + doorWidth, roomAHeight),
            topLeft + new Vector2(2.0f * roomAWidth + doorWallASmall, roomAHeight),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(2.0f * roomAWidth + doorWallASmall + doorWidth, roomAHeight),
            topLeft + new Vector2(houseWidth, roomAHeight),
            insideWallsWidth);

        factory.CreateWall(
            topLeft + new Vector2(0.0f, houseHeight - roomAHeight),
            topLeft + new Vector2(roomBWidth + doorWallASmall, houseHeight - roomAHeight),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(roomBWidth + doorWallASmall + doorWidth, houseHeight - roomAHeight),
            topLeft + new Vector2(roomAWidth + doorWallABig, houseHeight - roomAHeight),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(roomAWidth + doorWallABig + doorWidth, houseHeight - roomAHeight),
            topLeft + new Vector2(2.0f * roomAWidth + doorWallASmall, houseHeight - roomAHeight),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(2.0f * roomAWidth + doorWallASmall + doorWidth, houseHeight - roomAHeight),
            topLeft + new Vector2(houseWidth, houseHeight - roomAHeight),
            insideWallsWidth);

        factory.CreateWall(
            topLeft + new Vector2(roomAWidth, 0.0f),
            topLeft + new Vector2(roomAWidth, roomAHeight),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(2.0f * roomAWidth, 0.0f),
            topLeft + new Vector2(2.0f * roomAWidth, roomAHeight),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(roomAWidth, roomAHeight + roomBHeight),
            topLeft + new Vector2(roomAWidth, houseHeight),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(2.0f * roomAWidth, roomAHeight + roomBHeight),
            topLeft + new Vector2(2.0f * roomAWidth, houseHeight),
            insideWallsWidth);

        // B rooms
        factory.CreateWall(
            topLeft + new Vector2(roomBWidth, roomAHeight),
            topLeft + new Vector2(roomBWidth, roomAHeight + doorWallB),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(roomBWidth, roomAHeight + doorWallB + doorWidth),
            topLeft + new Vector2(roomBWidth, roomAHeight + 3.0f * doorWallB + doorWidth + roomCHeight),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(roomBWidth, roomAHeight + 3.0f * doorWallB + doorWidth + roomCHeight + doorWidth),
            topLeft + new Vector2(roomBWidth, roomAHeight + roomBHeight),
            insideWallsWidth);

        factory.CreateWall(
            topLeft + new Vector2(houseWidth - roomBWidth, roomAHeight),
            topLeft + new Vector2(houseWidth - roomBWidth, roomAHeight + doorWallB),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(houseWidth - roomBWidth, roomAHeight + doorWallB + doorWidth),
            topLeft + new Vector2(houseWidth - roomBWidth, roomAHeight + 3.0f * doorWallB + doorWidth + roomCHeight),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(houseWidth - roomBWidth, roomAHeight + 3.0f * doorWallB + doorWidth + roomCHeight + doorWidth),
            topLeft + new Vector2(houseWidth - roomBWidth, roomAHeight + roomBHeight),
            insideWallsWidth);

        // C rooms
        factory.CreateWall(
            topLeft + new Vector2(roomBWidth, roomAHeight + 2.0f * doorWallB + doorWidth),
            topLeft + new Vector2(roomBWidth + doorWallC, roomAHeight + 2.0f * doorWallB + doorWidth),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(roomBWidth + doorWallC + doorWidth, roomAHeight + 2.0f * doorWallB + doorWidth),
            topLeft + new Vector2(roomBWidth + roomCWidth + doorWallC, roomAHeight + 2.0f * doorWallB + doorWidth),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(roomBWidth + roomCWidth + doorWallC + doorWidth, roomAHeight + 2.0f * doorWallB + doorWidth),
            topLeft + new Vector2(roomBWidth + 2.0f * roomCWidth, roomAHeight + 2.0f * doorWallB + doorWidth),
            insideWallsWidth);

        factory.CreateWall(
            topLeft + new Vector2(roomBWidth, roomAHeight + 2.0f * doorWallB + doorWidth + roomCHeight),
            topLeft + new Vector2(roomBWidth + doorWallC, roomAHeight + 2.0f * doorWallB + doorWidth + roomCHeight),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(roomBWidth + doorWallC + doorWidth, roomAHeight + 2.0f * doorWallB + doorWidth + roomCHeight),
            topLeft + new Vector2(roomBWidth + roomCWidth + doorWallC, roomAHeight + 2.0f * doorWallB + doorWidth + roomCHeight),
            insideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(roomBWidth + roomCWidth + doorWallC + doorWidth, roomAHeight + 2.0f * doorWallB + doorWidth + roomCHeight),
            topLeft + new Vector2(roomBWidth + 2.0f * roomCWidth, roomAHeight + 2.0f * doorWallB + doorWidth + roomCHeight),
            insideWallsWidth);

        factory.CreateWall(
            topLeft + new Vector2(houseWidth / 2.0f, roomAHeight + 2 * doorWallB + doorWidth),
            topLeft + new Vector2(houseWidth / 2.0f, roomAHeight + 2 * doorWallB + doorWidth + roomCHeight),
            insideWallsWidth);
        #endregion
        #region Mansion Graph Definition
        var a1 = CreateDoorPoints(topLeft + new Vector2(doorOffsetASide, roomAHeight), false);
        var a2 = CreateDoorPoints(topLeft + new Vector2(roomAWidth + roomAWidth / 2.0f, roomAHeight), false);
        var a3 = CreateDoorPoints(topLeft + new Vector2(houseWidth - doorOffsetASide, roomAHeight), false);

        var a4 = CreateDoorPoints(topLeft + new Vector2(doorOffsetASide, houseHeight - roomAHeight), false);
        var a5 = CreateDoorPoints(topLeft + new Vector2(roomAWidth + roomAWidth / 2.0f, houseHeight - roomAHeight), false);
        var a6 = CreateDoorPoints(topLeft + new Vector2(houseWidth - doorOffsetASide, houseHeight - roomAHeight), false);

        var b1 = CreateDoorPoints(topLeft + new Vector2(roomBWidth, roomAHeight + hallHeight / 2.0f), true);
        var b2 = CreateDoorPoints(topLeft + new Vector2(roomBWidth, houseHeight - (roomAHeight + hallHeight / 2.0f)), true);

        var b3 = CreateDoorPoints(topLeft + new Vector2(houseWidth - roomBWidth, roomAHeight + hallHeight / 2.0f), true);
        var b4 = CreateDoorPoints(topLeft + new Vector2(houseWidth - roomBWidth, houseHeight - (roomAHeight + hallHeight / 2.0f)), true);

        var c1 = CreateDoorPoints(topLeft + new Vector2(roomBWidth + roomCWidth / 2.0f, roomAHeight + hallHeight), false);
        var c2 = CreateDoorPoints(topLeft + new Vector2(roomBWidth + roomCWidth / 2.0f, roomAHeight + hallHeight + roomCHeight), false);

        var c3 = CreateDoorPoints(topLeft + new Vector2(roomBWidth + roomCWidth / 2.0f + roomCWidth, roomAHeight + hallHeight), false);
        var c4 = CreateDoorPoints(topLeft + new Vector2(roomBWidth + roomCWidth / 2.0f + roomCWidth, roomAHeight + hallHeight + roomCHeight), false);

        // top A rooms
        CreateRoom(
            topLeft,
            new Vector2(roomAWidth, roomAHeight),
            a1);
        CreateRoom(
            topLeft + new Vector2(roomAWidth, 0.0f),
            new Vector2(roomAWidth, roomAHeight),
            a2);
        CreateRoom(
            topLeft + new Vector2(2.0f * roomAWidth, 0.0f),
            new Vector2(roomAWidth, roomAHeight),
            a3);

        // bottom A rooms
        CreateRoom(
            topLeft + new Vector2(0.0f, roomAHeight + roomBHeight),
            new Vector2(roomAWidth, roomAHeight),
            a4);
        CreateRoom(
            topLeft + new Vector2(roomAWidth, roomAHeight + roomBHeight),
            new Vector2(roomAWidth, roomAHeight),
            a5);
        CreateRoom(
            topLeft + new Vector2(2.0f * roomAWidth, roomAHeight + roomBHeight),
            new Vector2(roomAWidth, roomAHeight),
            a6);

        // B rooms
        CreateRoom(
            topLeft + new Vector2(0.0f, roomAHeight),
            new Vector2(roomBWidth, roomBHeight),
            Union(b1, b2));
        CreateRoom(
            topLeft + new Vector2(houseWidth - roomBWidth, roomAHeight),
            new Vector2(roomBWidth, roomBHeight),
            Union(b3, b4));

        // C rooms
        CreateRoom(
            topLeft + new Vector2(roomBWidth, roomAHeight + hallHeight),
            new Vector2(roomCWidth, roomCHeight),
            Union(c1, c2));
        CreateRoom(
            topLeft + new Vector2(roomBWidth + roomCWidth, roomAHeight + hallHeight),
            new Vector2(roomCWidth, roomCHeight),
            Union(c3, c4));

        // halls
        CreateRoom(
            topLeft + new Vector2(roomBWidth, roomAHeight),
            new Vector2(hallWidth, hallHeight),
            Union(a1, Union(a2, Union(a3, Union(b1, Union(c1, Union(c3, b3)))))));

        CreateRoom(
            topLeft + new Vector2(roomBWidth, roomAHeight + hallHeight + roomCHeight),
            new Vector2(hallWidth, hallHeight),
            Union(a4, Union(a5, Union(a6, Union(b2, Union(c2, Union(c4, b4)))))));
        #endregion
    }

    private List<Vector2> CreateDoorPoints(Vector2 doorPosition, bool vertical)
    {
        List<Vector2> points = new();

        if (vertical)
        {
            points.Add(doorPosition - new Vector2(insideWallsWidth / 2.0f + characterSize / 2.0f, 0.0f));
            points.Add(doorPosition + new Vector2(insideWallsWidth / 2.0f + characterSize / 2.0f, 0.0f));
        }
        else
        {
            points.Add(doorPosition - new Vector2(0.0f, insideWallsWidth / 2.0f + characterSize / 2.0f));
            points.Add(doorPosition + new Vector2(0.0f, insideWallsWidth / 2.0f + characterSize / 2.0f));
        }
        AddNode(points[0]);
        AddNode(points[1]);
        Graph.AddEdge(points[0].ToPoint(), points[1].ToPoint());

        return points;
    }

    private static List<Vector2> Union(List<Vector2> l1, List<Vector2> l2)
        => l1.Union(l2).ToList();

    private void CreateFloor()
    {
        const float tileSize = 32.0f;

        for (float x = -houseWidth / 2.0f; x < houseWidth / 2.0f; x += tileSize)
        {
            for (float y = -houseHeight / 2.0f; y < houseHeight / 2.0f; y += tileSize)
            {
                factory.CreateFloorTile(new Vector2(x + tileSize / 2.0f, y + tileSize / 2.0f));
            }
        }
    }

    private void CreateRoom(Vector2 offset, Vector2 size, List<Vector2> doors)
    {
        List<Point> nodes = new(nodesPerRoom);
        for (int i = 0; i < nodes.Capacity; i++)
        {
            AddNode(
                game.Random.NextVector2(
                    offset + new Vector2(characterSize) / 2.0f,
                    size - new Vector2(characterSize)),
                nodes);
        }

        Rectangle rectangle = new(offset.ToPoint(), size.ToPoint());
        foreach (var doorPosition in doors)
        {
            if (!rectangle.Contains(doorPosition))
                continue;

            AddNode(doorPosition, nodes);
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            for (int j = i + 1; j < nodes.Count; j++)
            {
                Graph.AddEdge(nodes[i], nodes[j]);
            }
        }
    }

    private void AddNode(Vector2 position, List<Point> points = null)
    {
        Point point = position.ToPoint();

        points?.Add(point);

        if (Graph.Contains(point))
            return;

        Graph.AddNode(point);
    }
}
