using MonoGamePlus.Events.Events;

using System;
using System.Collections;
using System.Collections.Generic;

namespace MonoGamePlus.Structures;
/// <summary>
/// Buffers added/removed items and adds/removes them only after <see cref="Update"/> is called. Otherwise behaves
/// same as <see cref="List{T}"./>
/// </summary>
internal class BufferedList<T> : ICollection<T>, IEnumerable<T>, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>
{
    /// <summary>
    /// Internal list.
    /// </summary>
    private readonly List<T> list = new();
    /// <summary>
    /// Buffer for items to add.
    /// </summary>
    private readonly List<T> itemsToAdd = new();
    /// <summary>
    /// Buffer for items to remove.
    /// </summary>
    private readonly List<T> itemsToRemove = new();

    public T this[int index]
    {
        get => list[index];
        set => list[index] = value;
    }

    public int Count => list.Count;

    public bool IsReadOnly => (list as IList<T>).IsReadOnly;

    public event BufferedListEventHandler<T> OnItemAdd;
    public event BufferedListEventHandler<T> OnItemRemove;

    /// <summary>
    /// Adds/removes buffered items. 
    /// </summary>
    public void Update()
    {
        foreach (var item in itemsToRemove)
        {
            OnItemRemove?.Invoke(this, new BufferedListEventArgs<T>(item));
            list.Remove(item);
        }
        itemsToRemove.Clear();

        foreach (var item in itemsToAdd)
        {
            OnItemAdd?.Invoke(this, new BufferedListEventArgs<T>(item));
            list.Add(item);
        }
        itemsToAdd.Clear();
    }

    public void Add(T item) => itemsToAdd.Add(item);

    /// <summary>
    /// Clear will also clear buffers.
    /// </summary>
    public void Clear()
    {
        list.Clear();
        itemsToAdd.Clear();
        itemsToRemove.Clear();
    }

    /// <summary>
    /// Contains only check internal list and not buffers.
    /// </summary>
    public bool Contains(T item) => list.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

    public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

    public int IndexOf(T item) => list.IndexOf(item);

    public void Insert(int index, T item)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Always returns true.
    /// </summary>
    public bool Remove(T item)
    {
        itemsToRemove.Add(item);
        return true;
    }

    public void RemoveAt(int index)
    {
        throw new NotSupportedException();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
