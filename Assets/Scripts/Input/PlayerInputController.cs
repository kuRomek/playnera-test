using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _draggableLayer;

        private PlayerInput _input;

        public event Action<Draggable, Vector3> DragStarted;
        public event Action<Vector3> Dragging;
        public event Action DragCanceled;

        private void Awake()
        {
            _input = new PlayerInput();
        }

        private void OnEnable()
        {
            _input.Enable();

            _input.Player.Press.performed += OnDragStarted;
            _input.Player.Drag.performed += OnDragging;
            _input.Player.Press.canceled += OnDragCanceled;
        }

        private void OnDisable()
        {
            _input.Disable();

            _input.Player.Press.performed -= OnDragStarted;
            _input.Player.Drag.performed -= OnDragging;
            _input.Player.Press.canceled -= OnDragCanceled;
        }

        private void OnDragStarted(InputAction.CallbackContext context)
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

            Draggable draggableObject = null;

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, float.PositiveInfinity, _draggableLayer);

            if (hit.collider != null)
                draggableObject = hit.collider.GetComponentInParent<Draggable>();

            DragStarted?.Invoke(draggableObject, mousePosition);
        }

        private void OnDragging(InputAction.CallbackContext context)
        {
            Dragging?.Invoke(_camera.ScreenToWorldPoint(context.action.ReadValue<Vector2>()));
        }

        private void OnDragCanceled(InputAction.CallbackContext context)
        {
            DragCanceled?.Invoke();
        }
    }
}
