using UnityEngine;

public class TreeRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _linePrefab;

    public void RedrawTree(Node root)
    {
        EraseTree();
        DrawTree(root);
    }

    private void DrawTree(Node root)
    {
        DrawLineToEachChild(root);

        foreach(Node child in root.Children)
        {
            DrawTree(child);
        }
    }

    private void DrawLineToEachChild(Node node)
    {
        foreach(Node child in node.Children)
        {
            LineRenderer line = Instantiate(
                original: _linePrefab,
                parent: transform);

            line.SetPositions(new Vector3[]
            {
                node.Position,
                child.Position
            });
        }
    }

    private void EraseTree()
    {
        foreach (Transform line in transform)
        {
            Destroy(line.gameObject);
        }
    }
}
