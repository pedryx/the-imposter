using Microsoft.Xna.Framework;

namespace TheImposter.GameStates;
internal class WorldGenerator
{
    private const float doorWidth = 200.0f;
    private const float houseWidth = 6000.0f;
    private const float houseHeight = 4000.0f;

    private const float outsideWallsWidth = 50.0f;
    private const float insideWallsWidth = 30.0f;

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

    private readonly LevelFactory factory;

    public WorldGenerator(LevelFactory factory)
    {
        this.factory = factory;
    }

    public void Generate()
    {
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

        /*factory.CreateWall(
            topLeft + new Vector2(0.0f, roomAHeight - insideWallsWidth),
            topLeft + new Vector2(roomBWidth, 0.0f),
            outsideWallsWidth);
        factory.CreateWall(
            topLeft + new Vector2(0.0f, houseHeight - roomAHeight),
            topLeft + new Vector2(houseWidth, houseHeight),
            outsideWallsWidth);*/
    }
}
