using Arch.Core;
using Arch.Core.Extensions;

using Microsoft.Xna.Framework;

using System;

namespace MonoGamePlus;
/// <summary>
/// Simple 2D camera.
/// </summary>
public class Camera
{
    private readonly MGPGame game;
    private readonly Random random = new();

    /// <summary>
    /// Time elapsed from the moment shake effect started.
    /// </summary>
    private float shakeElapsed;
    /// <summary>
    /// Duration of the shake effect.
    /// </summary>
    private float shakeDuration;
    /// <summary>
    /// Magnitude of shake effect.
    /// </summary>
    private float shakeMagnitude;
    /// <summary>
    /// Determine if shake effect is currently active.
    /// </summary>
    private bool shakeActive;

    /// <summary>
    /// Target to follow or null to not follow any target.
    /// </summary>
    public Entity? Target;

    public Vector2 Position { get; set; }
    public float Scale { get; set; } = 1.0f;
    public float Rotation { get; set; }

    public Camera(MGPGame game)
    {
        this.game = game;
    }

    /// <summary>
    /// Start screen shake effect.
    /// </summary>
    /// <param name="duration">Effect duration.</param>
    /// <param name="magnitude">Effect magnitude.</param>
    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        shakeActive = true;
    }

    internal void Update(float elapsed)
    {
        if (Target != null && Target.Value.Has<Vector2>())
            Position = Target.Value.Get<Vector2>();

        if (shakeActive)
        {
            Position += random.NextUnitVector() * shakeMagnitude;

            shakeElapsed += elapsed * game.Speed;
            if (shakeElapsed >= shakeDuration)
            {
                shakeElapsed = 0.0f;
                shakeActive = false;
            }
        }
    }

    public Matrix GetTransformMatrix()
        => Matrix.CreateTranslation(-Position.X, -Position.Y, 0.0f)
        * Matrix.CreateScale(Scale, Scale, 1.0f)
        * Matrix.CreateRotationZ(Rotation)
        * Matrix.CreateTranslation(game.Resolution.X / 2.0f, game.Resolution.Y / 2.0f, 0.0f);
}
