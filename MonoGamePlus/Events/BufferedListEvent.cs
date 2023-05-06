using MonoGamePlus.Structures;

namespace MonoGamePlus.Events.Events;
/// <summary>
/// Handler for events related with <see cref="BufferedList{T}"./>
/// </summary>
/// <param name="sender">Event sender.</param>
/// <param name="e">Event arguments.</param>
public delegate void BufferedListEventHandler<T>(object sender, BufferedListEventArgs<T> e);

/// <summary>
/// Arguments for events related with <see cref="BufferedList{T}"./>
/// </summary>
public class BufferedListEventArgs<T>
{
    /// <summary>
    /// Affected item.
    /// </summary>
    public T Item { get; private set; }

    public BufferedListEventArgs(T item)
    {
        Item = item;
    }
}
