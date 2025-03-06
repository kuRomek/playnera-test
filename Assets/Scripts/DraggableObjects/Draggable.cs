using Input;
using UnityEngine;

public abstract class Draggable : MonoBehaviour
{
    [SerializeField] private PlayerInputController _inputController;

    private Rigidbody2D _rigidbody;
    private Vector3 _lastMousePosition;
    private bool _isDragging = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
        if (draggingObject == this)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector2.zero;
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
            transform.position += new Vector3(delta.x, delta.y, 0f);

            _lastMousePosition = newMousePosition;
        }
    }

    private void OnDragCanceled()
    {
        if (_isDragging)
        {
            _rigidbody.isKinematic = false;
            _isDragging = false;
        }
    }
}
