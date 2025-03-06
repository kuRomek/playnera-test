using Input;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputController _inputController;
    [SerializeField] private SpriteRenderer _background;

    private bool _isDragging = false;
    private Vector3 _lastMousePosition;
    private float _leftBound;
    private float _rightBound;

    private void Awake()
    {
        Camera camera = GetComponent<Camera>();

        _leftBound = _background.bounds.min.x + camera.orthographicSize * camera.aspect;
        _rightBound = _background.bounds.max.x - camera.orthographicSize * camera.aspect;
    }

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
            return;

        mousePosition -= transform.position;
        _lastMousePosition = mousePosition;
        _isDragging = true;
    }

    private void Drag(Vector3 mousePosition)
    {
        if (_isDragging)
        {
            mousePosition -= transform.position;

            Vector3 deltaMouse = mousePosition - _lastMousePosition;

            Vector3 newPosition = transform.position + deltaMouse.x * Vector3.left;

            if (newPosition.x >= _leftBound && newPosition.x <= _rightBound)
            {
                transform.position = newPosition;
                _lastMousePosition = mousePosition;
            }
        }
    }

    private void OnDragCanceled()
    {
        _isDragging = false;
    }
}
