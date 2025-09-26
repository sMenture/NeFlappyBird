using System;
using System.Collections;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 15;

    private Coroutine _coroutine;

    public event Action LifeEnd;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(LifeCycle());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(_lifeTime);

        LifeEnd?.Invoke();
    }
}
