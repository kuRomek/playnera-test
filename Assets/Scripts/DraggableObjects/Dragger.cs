using Input;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    [SerializeField] private PlayerInputController _inputController;

    private Draggable _draggingObject = null;
    private Vector3 _lastMousePosition;
    private bool _isDragging = false;

    private void OnEnable()
    {
        _inputController.DragStarted += OnDragStarted;
        _inputController.Dragging += Drag;
        _inputController.DragCanceled += OnDragCanceled;
    }

    private void OnDisable()
    {
        _inputController.DragStarted -= OnDragStarted;
        _inputController.Dragging -= Drag;
        _inputController.DragCanceled -= OnDragCanceled;
    }

    private void OnDragStarted(Draggable draggingObject, Vector3 mousePosition)
    {
        if (draggingObject != null)
        {
            _draggingObject = draggingObject;
            _draggingObject.SetDraggingMode();
            _lastMousePosition = mousePosition;
            _isDragging = true;
        }
    }

    private void Drag(Vector3 mousePosition)
    {
        if (_isDragging)
        {
            Vector3 newMousePosition = mousePosition;
            Vector3 delta = mousePosition - _lastMousePosition;

            _draggingObject.transform.position += new Vector3(delta.x, delta.y, 0f);

            _lastMousePosition = newMousePosition;
        }
    }

    private void OnDragCanceled()
    {
        if (_isDragging)
        {
            _draggingObject.RemoveDraggingMode();
            _isDragging = false;
        }
    }
}
