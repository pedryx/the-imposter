namespace MonoGamePlus.Components;
/// <summary>
/// Entities with this tag belongs to static render layer. All entities in this layer are rendered after others
/// (event the <see cref="Foreground"/> layer) and are not affected by the camera.
/// </summary>
public struct Static { }
