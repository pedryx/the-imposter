using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamePlus;
/// <summary>
/// Contains math utility functions.
/// </summary>
public static class MathUtils
{
    /// <summary>
    /// Normalize angle so its in interval [0, 2pi).
    /// </summary>
    /// <param name="angle">Angle to normalize.</param>
    /// <returns>Normalized angle.</returns>
    public static float NormalizeAngle(float angle)
    {
        if (angle < 0.0f)
            angle += 2 * MathF.PI;
        if (angle >= 2 * MathF.PI)
            angle -= 2 * MathF.PI;

        return angle;
    }

    /// <summary>
    /// Convert angle to unit vector.
    /// </summary>
    /// <param name="angle">Angle to convert.</param>
    /// <returns>Resulting unit vector.</returns>
    public static Vector2 AngleToVector(float angle)
        => new(MathF.Cos(angle), MathF.Sin(angle));

    /// <summary>
    /// Convert unit vector to angle.
    /// </summary>
    /// <param name="vector">Unit vector to convert.</param>
    /// <returns>Resulting angle.</returns>
    public static float VectorToAngle(Vector2 vector)
        => MathF.Atan2(vector.Y, vector.X);
}
