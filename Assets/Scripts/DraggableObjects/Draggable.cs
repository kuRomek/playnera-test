using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Draggable : MonoBehaviour
{
    private const string DraggableLayer = "Draggable";

    private Rigidbody2D _rigidbody;
    private float _defaultGravityScale = 5f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = _defaultGravityScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(DraggableLayer))
            return;

        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(DraggableLayer))
            return;

        _rigidbody.isKinematic = false;
    }

    public void SetDraggingMode()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.gravityScale = 0f;
        _rigidbody.isKinematic = true;
    }

    public void RemoveDraggingMode()
    {
        if (_rigidbody.isKinematic)
            return;

        _rigidbody.gravityScale = _defaultGravityScale;
        _rigidbody.isKinematic = false;
    }
}
