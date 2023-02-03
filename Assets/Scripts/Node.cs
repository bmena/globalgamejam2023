using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node
{
    public Vector2 Position;
    public List<Node> Children;

    public Node(Vector2 position, params Node[] children)
    {
        Position = position;
        Children = children.ToList();
    }

    public Node(float x, float y, params Node[] children)
    {
        Position = new Vector2(x, y);
        Children = children.ToList();
    }

    public void ForEach(Action<Node> action)
    {
        action(this);

        foreach(Node child in Children)
        {
            child.ForEach(action);
        }
    }

    public float Distance(Vector2 position)
    {
        return Vector2.Distance(Position, position);
    }
}
