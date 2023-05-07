using System;

namespace MonoGamePlus.Systems;
public class DelayedStartSystem : GameSystem
{
    private float delay;

    public event EventHandler OnDelayedStart;

    public DelayedStartSystem(float delay)
    {
        this.delay = delay;
    }

    protected override void Update(float elapsed)
    {
        delay -= elapsed;

        if (delay <= 0.0f)
        {
            OnDelayedStart?.Invoke(this, new EventArgs());
            GameState.RemoveUpdateSystem(this);
            OnDelayedStart = null;
        }

        base.Update(elapsed);
    }
}
