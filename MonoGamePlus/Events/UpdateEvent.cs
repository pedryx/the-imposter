namespace MonoGamePlus.Events;
public delegate void UpdateEventHandler(object sender, UpdateEventArgs e);

public class UpdateEventArgs
{
    public float Elapsed;

    public UpdateEventArgs(float elapsed)
    {
        Elapsed = elapsed;
    }
}
