using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGamePlus.Resources;
using MonoGamePlus.Structures;

using System;
using System.Collections.Generic;

namespace MonoGamePlus;
/// <summary>
/// Represent main game class.
/// </summary>
public class MGPGame : Game
{
    /// <summary>
    /// Contains game states which are currently active,
    /// </summary>
    private readonly BufferedList<GameState> activeStates = new();

    /// <summary>
    /// Game states which are currently active.
    /// </summary>
    public IReadOnlyList<GameState> ActiveStates => activeStates;
    /// <summary>
    /// Width and height of game window.
    /// </summary>
    public Vector2 Resolution => new(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);

    /// <summary>
    /// Master seed for random number generation.
    /// </summary>
    public int Seed { get; private set; }
    /// <summary>
    /// Graphics settings.
    /// </summary>
    public GraphicsDeviceManager Graphics { get; private set; }
    /// <summary>
    /// Sprite batch used for rending textures and fonts.
    /// </summary>
    public SpriteBatch SpriteBatch { get; private set; }
    /// <summary>
    /// Random number generator created with master seed. (<see cref="Seed"/>)
    /// </summary>
    public Random Random { get; private set; }
    public TextureManager Textures { get; private set; }
    public SoundManager Sounds { get; private set; }
    public FontManager Fonts { get; private set; }
    // TODO: statistics

    /// <summary>
    /// Color used for clearing graphics buffer at the start of <see cref="Draw(GameTime)"/>.
    /// </summary>
    public Color ClearColor { get; set; } = Color.Black;
    /// <summary>
    /// How fast is the game running, changing this value will not affect performance.
    /// </summary>
    public float Speed { get; set; } = 1.0f;

    /// <summary>
    /// Create new MonoGamePlus Game.
    /// </summary>
    /// <param name="seed">Master seed for random number generation.</param>
    public MGPGame(Vector2 resolution, int seed = 0)
    {
        Seed = seed;
        Random = new Random(Seed);
        Graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = (int)resolution.X,
            PreferredBackBufferHeight = (int)resolution.Y,
        };

        IsMouseVisible = true;

        activeStates.OnItemAdd += (sender, e) => e.Item.Initialize(this);
        activeStates.OnItemRemove += (sender, e) => e.Item.Destroy();
    }

    /// <summary>
    /// Do not use this method to start the game. This method hides basic MonoGame start method which is
    /// used to start MonoGame game and does not set initial game state. To start the game use generic version
    /// of this method instead. Calling this method will throw <see cref="InvalidOperationException"/>.
    /// </summary>
    public static new void Run()
    {
        throw new InvalidOperationException("This method was used to start basic MonoGame game which does not" +
            "set initial game state. To start the game use generic version of this method instead.");
    }

    /// <summary>
    /// Start the game with initial game state.
    /// </summary>
    /// <typeparam name="TGameState">Initial game state.</typeparam>
    public void Run<TGameState>()
        where TGameState : GameState, new()
    {
        activeStates.Add(new TGameState());
        base.Run();
    }

    /// <summary>
    /// Adds game state to <see cref="ActiveStates"/>. Game state will be added at the end of
    /// <see cref="Update(GameTime)"/>.
    /// </summary>
    /// <param name="gameState">Game state to add.</param>
    public void AddGameState(GameState gameState)
        => activeStates.Add(gameState);

    /// <summary>
    /// Remove game state from <see cref="ActiveStates"/>. Game state will be removed at the end of
    /// <see cref="Update(GameTime)"/>.
    /// </summary>
    /// <param name="gameState">Game state to remove.</param>
    public void RemoveGameState(GameState gameState)
        => activeStates.Remove(gameState);

    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        Textures = new TextureManager(this);
        Sounds = new SoundManager(this);
        Fonts = new FontManager(this);

        Textures.LoadAll();
        Sounds.LoadAll();
        Fonts.LoadAll();

        foreach (var gameState in activeStates)
        {
            gameState.Initialize(this);
        }

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

        foreach (var gameState in activeStates)
        {
            gameState.Update(elapsed);
        }
        activeStates.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

        GraphicsDevice.Clear(ClearColor);

        foreach (var gameState in activeStates)
        {
            gameState.Draw(elapsed);
        }

        base.Draw(gameTime);
    }
}
