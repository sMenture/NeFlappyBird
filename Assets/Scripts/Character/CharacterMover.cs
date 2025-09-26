using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
    }

    public void Move(Vector2 inputDirection)
    {
        _rigidbody2D.linearVelocity = inputDirection * _speed;
    }
}
