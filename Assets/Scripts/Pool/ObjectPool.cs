using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _spawnCount = 50;

    private Queue<T> _pooledObjects = new Queue<T>();

    public int SpawnCount => _spawnCount;
    public int PoolCount => _pooledObjects.Count;
    public bool CanReturnDequeueElememt => _pooledObjects.Count > 0;

    public event Action AddElement;
    public event Action RemoveElement;

    private void Awake()
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            var newPoolElement = Instantiate(_prefab, transform);
            newPoolElement.gameObject.SetActive(false);

            _pooledObjects.Enqueue(newPoolElement);
        }
    }

    public T GiveElement()
    {
        var firstPoolElement = _pooledObjects.Dequeue();

        firstPoolElement.gameObject.SetActive(true);

        AddElement?.Invoke();

        return firstPoolElement;
    }

    public void ReturnToPool(T element)
    {
        element.gameObject.SetActive(false);
        _pooledObjects.Enqueue(element);

        RemoveElement?.Invoke();
    }
}