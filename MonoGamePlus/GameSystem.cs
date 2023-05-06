using Arch.Core;

namespace MonoGamePlus;

/// <summary>
/// General game system.
/// </summary>
public abstract class GameSystem
{
    public MGPGame Game { get; private set; }
    public GameState GameState { get; private set; }
    public World ECSWorld { get; private set; }

    internal void Initialize(GameState gameState)
    {
        Game = gameState.Game;
        GameState = gameState;
        ECSWorld = gameState.ECSWorld;

        Initialize();
    }

    /// <summary>
    /// Run one iteration of game system.
    /// </summary>
    internal void Run(float elapsed)
    {
        PreUpdate(elapsed);
        Update(elapsed);
        PostUpdate(elapsed);
    }

    protected virtual void Initialize() { }
    protected virtual void PreUpdate(float elapsed) { }
    protected virtual void Update(float elapsed) { }
    protected virtual void PostUpdate(float elapsed) { }
}

/// <summary>
/// Game system which handles component tuples with size one.
/// </summary>
public class GameSystem<T1> : GameSystem
    where T1 : struct
{
    protected QueryDescription Query { get; private set; }

    public GameSystem()
    {
        Query = new QueryDescription().WithAll<T1>();
    }

    protected override void Update(float elapsed)
    {
        ECSWorld.Query(Query, (in Entity entity, ref T1 component1) =>
        {
            Update(elapsed, in entity, ref component1);
        });

        base.Update(elapsed);
    }

    protected virtual void Update(float elapsed, in Entity entity, ref T1 component1) { }
}

/// <summary>
/// Game system which handles component tuples with size two.
/// </summary>
public class GameSystem<T1, T2> : GameSystem
    where T1 : struct
    where T2 : struct
{
    protected QueryDescription Query { get; private set; }


    public GameSystem()
    {
        Query = new QueryDescription().WithAll<T1, T2>();
    }

    protected override void Update(float elapsed)
    {
        ECSWorld.Query(Query, (in Entity entity, ref T1 component1, ref T2 component2) =>
        {
            Update(elapsed, in entity, ref component1, ref component2);
        });

        base.Update(elapsed);
    }

    protected virtual void Update(float elapsed, in Entity entity, ref T1 component1, ref T2 component2) { }
}

/// <summary>
/// Game system which handles component tuples with size three.
/// </summary>
public class GameSystem<T1, T2, T3> : GameSystem
    where T1 : struct
    where T2 : struct
    where T3 : struct
{
    protected QueryDescription Query { get; private set; }


    public GameSystem()
    {
        Query = new QueryDescription().WithAll<T1, T2, T3>();
    }

    protected override void Update(float elapsed)
    {
        ECSWorld.Query(Query, (in Entity entity, ref T1 component1, ref T2 component2, ref T3 component3) =>
        {
            Update(elapsed, in entity, ref component1, ref component2, ref component3);
        });

        base.Update(elapsed);
    }

    protected virtual void Update(
        float elapsed,
        in Entity entity,
        ref T1 component1,
        ref T2 component2,
        ref T3 component3)
    { }
}

/// <summary>
/// Game system which handles component tuples with size four.
/// </summary>
public class GameSystem<T1, T2, T3, T4> : GameSystem
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
{
    protected QueryDescription Query { get; private set; }


    public GameSystem()
    {
        Query = new QueryDescription().WithAll<T1, T2, T3, T4>();
    }

    protected override void Update(float elapsed)
    {
        ECSWorld.Query(Query, (in Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) =>
        {
            Update(elapsed, in entity, ref component1, ref component2, ref component3, ref component4);
        });

        base.Update(elapsed);
    }

    protected virtual void Update(
        float elapsed,
        in Entity entity,
        ref T1 component1,
        ref T2 component2,
        ref T3 component3,
        ref T4 component4)
    { }
}