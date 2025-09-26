using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    [SerializeField] private float _speed = 3;
    [SerializeField] private float _lifeTime = 5;

    private GameObject _owner;

    private float _currentTime;
    private Rigidbody2D _rigidbody2D;
    private Coroutine _coroutine;

    public event Action<Bullet> IsHit;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    }

    public void SetOwner(GameObject owner)
    {
        _owner = owner;
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(LinearAccelerationToSide());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
        _owner = null;
    }

    private IEnumerator LinearAccelerationToSide()
    {
        _currentTime = _lifeTime;
        float speedByTime = 0;

        while (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;

            //speedByTime = _lifeTime - _currentTime;
            _rigidbody2D.linearVelocity = transform.up * _speed;

            yield return null;
        }

        IsHit?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _owner)
            return;

        if (collision.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(_damage);

        IsHit?.Invoke(this);
    }
}
