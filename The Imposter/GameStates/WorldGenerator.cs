using Microsoft.Xna.Framework;

using MonoGamePlus;

using System.Collections.Generic;

namespace TheImposter.GameStates;
internal class WorldGenerator
{
    private const float doorWidth = 200.0f;
    private const float houseWidth = 6000.0f;
    private const float houseHeight = 4000.0f;

    private const float outsideWallsWidth = 50.0f;
    private const float insideWallsWidth = 30.0f;

    private const int nodesPerRoomSize = 2;
    private const float characterSize = 60.0f;

    #region Mansion Constants
    private const float roomAWidth = houseWidth / 3.0f;
    private const float roomAHeight = houseHeight / 5.0f;
    private const float doorWallASmall = (roomAWidth - roomBWidth - doorWidth) / 2.0f;
    private const float doorWallABig = (roomAWidth - doorWidth) / 2.0f;

    private const float roomBWidth = houseWidth / 5.0f;
    private const float roomBHeight = houseHeight  - 2.0f * roomAHeight;
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

    private Graph graph;

    public WorldGenerator(LevelFactory factory, GameState gameState)
    {
        this.factory = factory;
        game = gameState.Game;
    }

    public Graph Generate(int characterCount)
    {
        graph = new Graph();

        CreateMansion();

        for (int i = 0; i < characterCount; i++)
        {
            factory.CreateNPC(graph.GetRandomNode(game.Random).ToVector2(), Color.Orange);
        }

        return graph;
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
        // top A rooms
        CreateRoom(
            topLeft,
            new Vector2(roomAWidth, roomAHeight),
            new List<Vector2>()
            {
                topLeft + new Vector2(doorOffsetASide, roomAHeight),
            });
        CreateRoom(
            topLeft + new Vector2(roomAWidth, 0.0f),
            new Vector2(roomAWidth, roomAHeight),
            new List<Vector2>()
            {
                topLeft + new Vector2(roomAWidth + roomAWidth / 2.0f, roomAHeight),
            });
        CreateRoom(
            topLeft + new Vector2(2.0f * roomAWidth, 0.0f),
            new Vector2(roomAWidth, roomAHeight),
            new List<Vector2>()
            {
                topLeft + new Vector2(houseWidth - doorOffsetASide, roomAHeight),
            });

        // bottom A rooms
        CreateRoom(
            topLeft + new Vector2(0.0f, roomAHeight + roomBHeight),
            new Vector2(roomAWidth, roomAHeight),
            new List<Vector2>()
            {
                topLeft + new Vector2(doorOffsetASide, houseHeight - roomAHeight),
            });
        CreateRoom(
            topLeft + new Vector2(roomAWidth, roomAHeight + roomBHeight),
            new Vector2(roomAWidth, roomAHeight),
            new List<Vector2>()
            {
                topLeft + new Vector2(roomAWidth + roomAWidth / 2.0f, houseHeight - roomAHeight),
            });
        CreateRoom(
            topLeft + new Vector2(2.0f * roomAWidth, roomAHeight + roomBHeight),
            new Vector2(roomAWidth, roomAHeight),
            new List<Vector2>()
            {
                topLeft + new Vector2(houseWidth - doorOffsetASide, houseHeight - roomAHeight),
            });

        // B rooms
        CreateRoom(
            topLeft + new Vector2(0.0f, roomAHeight),
            new Vector2(roomBWidth, roomBHeight),
            new List<Vector2>()
            {
                topLeft + new Vector2(roomBWidth, roomAHeight + hallHeight / 2.0f),
                topLeft + new Vector2(roomBWidth, houseHeight - (roomAHeight + hallHeight / 2.0f)),
            });
        CreateRoom(
            topLeft + new Vector2(houseWidth - roomBWidth, roomAHeight),
            new Vector2(roomBWidth, roomBHeight),
            new List<Vector2>()
            {
                topLeft + new Vector2(houseWidth - roomBWidth, roomAHeight + hallHeight / 2.0f),
                topLeft + new Vector2(houseWidth - roomBWidth, houseHeight - (roomAHeight + hallHeight / 2.0f)),
            });

        // C rooms
        CreateRoom(
            topLeft + new Vector2(roomBWidth, roomAHeight + hallHeight),
            new Vector2(roomCWidth, roomCHeight),
            new List<Vector2>()
            {
                topLeft + new Vector2(roomBWidth + roomCWidth / 2.0f, roomAHeight + hallHeight),
                topLeft + new Vector2(roomBWidth + roomCWidth / 2.0f, roomAHeight + hallHeight + roomCHeight),
            });
        CreateRoom(
            topLeft + new Vector2(roomBWidth + roomCWidth, roomAHeight + hallHeight),
            new Vector2(roomCWidth, roomCHeight),
            new List<Vector2>()
            {
                topLeft + new Vector2(roomBWidth + roomCWidth / 2.0f + roomCWidth, roomAHeight + hallHeight),
                topLeft + new Vector2(roomBWidth + roomCWidth / 2.0f + roomCWidth, roomAHeight + hallHeight + roomCHeight),
            });

        // halls
        CreateRoom(
            topLeft + new Vector2(roomBWidth, roomAHeight),
            new Vector2(hallWidth, hallHeight),
            new List<Vector2>()
            {
                topLeft + new Vector2(doorOffsetASide, roomAHeight),
                topLeft + new Vector2(roomAWidth + roomAWidth / 2.0f, roomAHeight),
                topLeft + new Vector2(houseWidth - doorOffsetASide, roomAHeight),
                topLeft + new Vector2(roomBWidth, roomAHeight + hallHeight / 2.0f),
                topLeft + new Vector2(houseWidth - roomBWidth, roomAHeight + hallHeight / 2.0f),
                topLeft + new Vector2(roomBWidth + roomCWidth / 2.0f, roomAHeight + hallHeight),
                topLeft + new Vector2(roomBWidth + roomCWidth / 2.0f + roomCWidth, roomAHeight + hallHeight),
            });

        CreateRoom(
            topLeft + new Vector2(roomBWidth, roomAHeight + hallHeight + roomCHeight),
            new Vector2(hallWidth, hallHeight),
            new List<Vector2>()
            {
                topLeft + new Vector2(roomBWidth + roomCWidth / 2.0f, roomAHeight + hallHeight + roomCHeight),
                topLeft + new Vector2(roomBWidth + roomCWidth / 2.0f + roomCWidth, roomAHeight + hallHeight + roomCHeight),
                topLeft + new Vector2(doorOffsetASide, houseHeight - roomAHeight),
                topLeft + new Vector2(roomAWidth + roomAWidth / 2.0f, houseHeight - roomAHeight),
                topLeft + new Vector2(houseWidth - doorOffsetASide, houseHeight - roomAHeight),
                topLeft + new Vector2(roomBWidth, houseHeight - (roomAHeight + hallHeight / 2.0f)),
                topLeft + new Vector2(houseWidth - roomBWidth, houseHeight - (roomAHeight + hallHeight / 2.0f)),
            });
        #endregion
    }

    private void CreateRoom(Vector2 offset, Vector2 size, List<Vector2> doors)
    {
        List<Point> nodes = new((int)((size.X * size.Y) / 10000.0f) * nodesPerRoomSize);
        for (int i = 0; i < nodes.Capacity; i++)
        {
            AddNode(game.Random.NextVector2(offset + new Vector2(characterSize), size - 2.0f * new Vector2(characterSize)), nodes);
        }

        foreach (var doorPosition in doors)
        {
            AddNode(doorPosition, nodes);
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            for (int j = i + 1; j < nodes.Count; j++)
            {
                graph.AddEdge(nodes[i], nodes[j]);
            }
        }
    }

    private void AddNode(Vector2 position, List<Point> points = null)
    {
        Point point = position.ToPoint();

        points?.Add(point);

        if (graph.Contains(point))
            return;

        graph.AddNode(point);
    }
}
