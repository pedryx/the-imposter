using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace MonoGamePlus;
internal class Node
{
    public Point Position { get; private set; }
    public List<Node> Neighbors { get; private set; } = new();

    public Node(Point position)
    {
        Position = position;
    }
}
