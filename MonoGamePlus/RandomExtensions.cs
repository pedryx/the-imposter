using Microsoft.Xna.Framework;

using System;

namespace MonoGamePlus;
/// <summary>
/// Contains extension method for <see cref="Random"/>.
/// </summary>
public static class RandomExtensions
{
    /// <summary>
    /// Generate random angle in range [0; 2pi).
    /// </summary>
    public static float NextAngle(this Random random)
        => random.NextSingle() * 2 * MathF.PI;

    /// <summary>
    /// Generate random unit vector.
    /// </summary>
    public static Vector2 NextUnitVector(this Random random)
        => MathUtils.AngleToVector(NextAngle(random));

    /// <summary>
    /// Generate random point int bounded space.
    /// </summary>
    /// <param name="bounds">Size of bounded space.</param>
    public static Vector2 Nextvector2(this Random random, Vector2 bounds)
        => new Vector2(random.NextSingle(), random.NextSingle()) * bounds;
    
    /// <summary>
    /// Generate single precision floating point number in specific range.
    /// </summary>
    /// <param name="minValue">Range start inclusive.</param>
    /// <param name="maxValue">Range end exclusive.</param>
    /// <returns>Generated value.</returns>
    public static float NextSingle(this Random random, float minValue, float maxValue)
        => (maxValue - minValue) * random.NextSingle() + minValue;
}
