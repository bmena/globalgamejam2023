using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    [SerializeField] private TreeRenderer _treeRenderer;

    private Node _tree =
        new Node(0, 0,
            new Node(0, 1));

    private void Start()
    {
        _treeRenderer.RedrawTree(_tree);
    }

    public void AddNewNode(Node parentNode, Vector2 position)
    {
        parentNode.Children.Add(new Node(position));

        _treeRenderer.RedrawTree(_tree);
    }

    public Node GetClosestNode(Vector2 position)
    {
        Node closestNode = _tree;

        _tree.ForEach(node =>
        {
            if (node.Distance(position) < closestNode.Distance(position))
            {
                closestNode = node;
            }
        });

        return closestNode;
    }
}
