﻿using Arch.Core;

using MonoGamePlus.Structures;

namespace MonoGamePlus;
/// <summary>
/// Each game state represent one game screen. Game state could be used to represent for example: menus, game over
/// screen, or main gameplay screen (often called level screen or level game state).
/// </summary>
public abstract class GameState
{
    /// <summary>
    /// Systems handled in <see cref="Update(float)"/>.
    /// </summary>
    private readonly BufferedList<GameSystem> updateSystems = new();
    /// <summary>
    /// Systems handled in <see cref="Draw(float)"/>.
    /// </summary>
    private readonly BufferedList<GameSystem> renderSystems = new();

    /// <summary>
    /// Game which owns game state.
    /// </summary>
    public MGPGame Game { get; private set; }
    /// <summary>
    /// ECS world used to manage entities of this game state.
    /// </summary>
    public World ECSWorld { get; private set; } = World.Create();
    /// <summary>
    /// Game state's main camera.
    /// </summary>
    public Camera Camera { get; private set; }
    // TODO: UI

    /// <summary>
    /// Enables/disables update calls of this game state.
    /// </summary>
    public bool Enable { get; set; } = true;
    /// <summary>
    /// Enables/disables draw calls of this game state.
    /// </summary>
    public bool Visible { get; set; } = true;

    public GameState()
    {
        updateSystems.OnItemAdd += Systems_OnItemAdd;
        renderSystems.OnItemAdd += Systems_OnItemAdd;
    }

    /// <summary>
    /// Add update system. Update systems <see cref="GameSystem.Run(float)"/> method is called in
    /// <see cref="Update(float)"/>.
    /// </summary>
    /// <param name="system">System to add.</param>
    public void AddUpdateSystem(GameSystem system) => updateSystems.Add(system);
    /// <summary>
    /// Add render system. Render systems <see cref="GameSystem.Run(float)"/> method is called in
    /// <see cref="Draw(float)"/>.
    /// </summary>
    /// <param name="system">System to add.</param>
    public void AddRenderSystem(GameSystem system) => renderSystems.Add(system);

    /// <summary>
    /// Initialize game state.
    /// </summary>
    /// <param name="game">Game which owns this game state.</param>
    protected internal void Initialize(MGPGame game)
    {
        Game = game;
        Camera = new Camera(game);

        Initialize();
    }

    internal void Update(float elapsed)
    {
        if (!Enable)
            return;

        foreach (var system in updateSystems)
        {
            system.Run(elapsed);
        }
        Camera.Update(elapsed);

        updateSystems.Update();
        renderSystems.Update();
    }

    internal void Draw(float elapsed)
    {
        if (!Visible)
            return;

        foreach (var system in renderSystems)
        {
            system.Run(elapsed);
        }
    }

    protected internal virtual void Destroy()
    {
        World.Destroy(ECSWorld);
    }

    /// <summary>
    /// Creates UI, systems and entities.
    /// </summary>
    protected virtual void Initialize() { }

    private void Systems_OnItemAdd(object sender, Events.BufferedListEventArgs<GameSystem> e)
    {
        e.Item.Initialize(this);
    }
}
