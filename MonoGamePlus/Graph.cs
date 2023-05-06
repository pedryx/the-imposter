using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoGamePlus;
public class Graph
{
    private readonly Dictionary<Point, Node> nodes = new();

    public void AddNode(Point position)
        => nodes.Add(position, new Node(position));

    public void AddEdge(Point position1, Point position2)
    {
        nodes[position1].Neighbors.Add(nodes[position2]);
        nodes[position2].Neighbors.Add(nodes[position1]);
    }

    public bool Contains(Point position)
        => nodes.ContainsKey(position);

    public IEnumerable<Point> GetNeighbors(Point position)
        => nodes[position].Neighbors.Select(n => n.Position);

    public Point GetRandomNode(Random random)
        => nodes.ElementAt(random.Next(nodes.Count)).Value.Position;

    public Vector2[] FindPath(Point start, Point destination)
    {
        Node end = nodes[destination];
        HashSet<Node> visited = new() { nodes[start] };
        Queue<Node> frontier = new();
        Dictionary<Node, Node> cameFrom = new() { { nodes[start], null } };
        frontier.Enqueue(nodes[start]);

        while (frontier.Any())
        {
            Node current = frontier.Dequeue();

            if (current == end)
            {
                List<Vector2> path = new();

                while (current != null)
                {
                    path.Add(current.Position.ToVector2());
                    current = cameFrom[current];
                }

                path.Reverse();
                return path.ToArray();
            }

            foreach (Node neighbor in current.Neighbors)
            {
                if (visited.Contains(neighbor))
                    continue;

                visited.Add(neighbor);
                frontier.Enqueue(neighbor);
                cameFrom[neighbor] = current;
            }
        }

        throw new InvalidOperationException("Path does not exist!");
    }
}
