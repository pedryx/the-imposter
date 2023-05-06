﻿using Microsoft.Xna.Framework;

namespace MonoGamePlus;
public struct Circle
{
    public Vector2 Center;
    public float Radius;

    public Circle(Vector2 center, float radius)
    {
        Center = center;
        Radius = radius;
    }
}
