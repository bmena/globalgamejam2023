using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private enum State
    {
        Hovering,
        Dragging,
        Released
    }

    [SerializeField] private Transform _parentNodeIndicatorPrefab;
    [SerializeField] private Transform _newNodeIndicatorPrefab;
    [SerializeField] private LineRenderer _previewLinePrefab;
    [SerializeField] private TreeManager _treeManager;

    private Node _parentNode;

    private State _state;

    private Transform _parentNodeIndicator;
    private Transform _newNodeIndicator;
    private LineRenderer _previewLine;

    private void Awake()
    {
        _parentNodeIndicator = Instantiate(
            original: _parentNodeIndicatorPrefab,
            parent: transform);
        _parentNodeIndicator.gameObject.SetActive(false);

        _newNodeIndicator = Instantiate(
            original: _newNodeIndicatorPrefab,
            parent: transform);
        _newNodeIndicator.gameObject.SetActive(false);

        _previewLine = Instantiate(
            original: _previewLinePrefab,
            parent: transform);
        _previewLine.gameObject.SetActive(false);
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        switch(_state)
        {
            case State.Hovering:
                ChooseParentNode(mousePosition);
                break;
            case State.Dragging:
                ShowPreviewLine(mousePosition);
                break;
            case State.Released:
                CreateNewNode(_parentNode, mousePosition);
                break;
        }
    }

    private void ChooseParentNode(Vector2 mousePosition)
    {
        if (_parentNode == null ||
            (_parentNode.Distance(mousePosition) > 1))
        {
            Node closestNodeToMouse = _treeManager.GetClosestNode(mousePosition);

            if (closestNodeToMouse.Distance(mousePosition) < 2)
            {
                // HighlightNode
                _parentNodeIndicator.position = closestNodeToMouse.Position;
                _parentNodeIndicator.gameObject.SetActive(true);
                _parentNode = closestNodeToMouse;
            }
            else
            {
                // HideIndicator
                _parentNodeIndicator.gameObject.SetActive(false);
                _parentNode = null;
            }
        }

        if (_parentNode != null &&
            Input.GetMouseButtonDown(0))
        {
            _state = State.Dragging;
            _previewLine.gameObject.SetActive(true);
            _previewLine.SetPositions(new Vector3[] { _parentNode.Position, mousePosition });
            _newNodeIndicator.gameObject.SetActive(true);
        }
    }

    private void ShowPreviewLine(Vector2 mousePosition)
    {
        if (Input.GetMouseButtonUp(0))
        {
            _state = State.Released;
            _previewLine.gameObject.SetActive(false);
            _newNodeIndicator.gameObject.SetActive(false);
        }
        else
        {
            _previewLine.SetPosition(1, mousePosition);
            _newNodeIndicator.position = mousePosition;
        }
    }

    private void CreateNewNode(Node parentNode, Vector2 newNodePosition)
    {
        _treeManager.AddNewNode(parentNode, newNodePosition);
        _state = State.Hovering;
    }
}
