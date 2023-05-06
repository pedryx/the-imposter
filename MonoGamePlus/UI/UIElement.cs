using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

namespace MonoGamePlus.UI;
/// <summary>
/// Represent user interface element.
/// </summary>
public abstract class UIElement
{
    private readonly List<UIElement> childs = new();

    /// <summary>
    /// Offset from parent element or position of element if element is directly in ui layer.
    /// </summary>
    public Vector2 Offset;

    /// <summary>
    /// UI layer to which this element belong.
    /// </summary>
    public UILayer UILayer { get; private set; }

    public GameState GameState { get; private set; }
    public MGPGame Game { get; private set; }

    /// <summary>
    /// Get size of an element.
    /// </summary>
    public virtual Vector2 Size => throw new NotImplementedException();

    protected virtual void Initialize() { }

    /// <summary>
    /// Associate element with ui layer.
    /// </summary>
    public void Initialize(UILayer owner)
    {
        UILayer = owner;
        GameState = owner.GameState; ;
        Game = owner.GameState.Game;

        Initialize();
    }

    protected void AddChild(UIElement element)
    {
        element.Initialize(UILayer);
        childs.Add(element);
    }

    protected void RemoveChild(UIElement element)
        => childs.Remove(element);

    public virtual void Update(float elapsed, Vector2 position)
    {
        foreach (var child in childs)
        {
            child.Update(elapsed, position + child.Offset);
        }
    }

    public virtual void Draw(float elapsed, Vector2 position)
    {
        foreach (var child in childs)
        {
            child.Draw(elapsed, position + child.Offset);
        }
    }
}
