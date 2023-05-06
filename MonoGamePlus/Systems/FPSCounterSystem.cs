using System.Linq;

namespace MonoGamePlus.Systems;
public class FPSCounterSystem : GameSystem
{
    private readonly float[] times = new float[60];

    private int index;

    public float FPS { get; private set; }

    protected override void Update(float elapsed)
    {
        times[index] = elapsed;

        index++;
        if (index >= times.Length)
            index = 0;

        FPS = 1.0f / times.Average();

        Game.Window.Title = FPS.ToString();

        base.Update(elapsed);
    }
}
