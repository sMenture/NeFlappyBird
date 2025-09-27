using System.Collections.Generic;
using UnityEngine;

public class CharacterAttacker : MonoBehaviour
{
    [SerializeField] private CharacterAudioShot _audioShot;
    [SerializeField] private BulletPool _bulletPool;

    [Header("Settings")]
    [SerializeField, Range(0.1f, 10)] private float _delayBetweenShots = 1;
    [SerializeField] private Vector3 _bulletOffset;

    private float _currentTime;
    private Quaternion _basicRotation;

    private void Awake()
    {
        _basicRotation = transform.rotation;
    }

    public void Attack()
    {
        _currentTime -= Time.deltaTime;

        if (0 > _currentTime)
            Shot();
    }

    public void Initialize(BulletPool objectPool)
    {
        _bulletPool = objectPool;
    }

    private void Shot()
    {
        _currentTime = _delayBetweenShots;

        if (_bulletPool == null)
            return;

        if (_bulletPool.HasElements == false)
            return;

        if (_audioShot != null)
            _audioShot.Play();

        Bullet bullet = _bulletPool.GiveElement();
        bullet.IsHit += ReturnBulletToPool;
        bullet.SetOwner(gameObject);

        bullet.transform.position = transform.TransformPoint(_bulletOffset + Vector3.forward);
        bullet.transform.rotation = _basicRotation;    
    }

    private void ReturnBulletToPool(Bullet bullet)
    {
        bullet.IsHit -= ReturnBulletToPool;

        _bulletPool.ReturnToPool(bullet);
    }

    public void Reset()
    {
        foreach (var bullet in _bullets)
        {
            _bulletPool.ReturnToPool(bullet);
        }

        _bullets.Clear();
    }
}
