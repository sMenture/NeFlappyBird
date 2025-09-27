using System;
using UnityEngine;

[RequireComponent (typeof(EnemyLife))]
[RequireComponent (typeof(CharacterMover))]
[RequireComponent (typeof(CharacterHealth))]
[RequireComponent (typeof(EnemyModelChange))]
[RequireComponent (typeof(CharacterAttacker))]
public class Enemy : MonoBehaviour
{
    private EnemyLife _enemyLife;
    private CharacterMover _mover;
    private EnemyModelChange _modelChange;
    private CharacterHealth _health;

    private Vector2 _moveDirection = Vector2.left;

    public event Action<Enemy> Die;

    public CharacterAttacker Attack { get; private set; }

    private void Awake()
    {
        _enemyLife = GetComponent<EnemyLife>();
        _mover = GetComponent<CharacterMover>();
        _health = GetComponent<CharacterHealth>();
        Attack = GetComponent<CharacterAttacker>();
        _modelChange = GetComponent<EnemyModelChange>();
    }

    private void Start()
    {
        _modelChange.SetRandomModel();
    }

    private void OnEnable()
    {
        _enemyLife.LifeEnd += LifeEnd;
        _health.Change += HealthChange;
    }

    private void OnDisable()
    {
        _enemyLife.LifeEnd -= LifeEnd;
        _health.Change -= HealthChange;
    }

    private void Update()
    {
        _mover.Move(_moveDirection);
        Attack.Attack();
    }

    private void HealthChange(int value)
    {
        if (_health.IsAlive)
            return;

        Die?.Invoke(this);
    }
    private void LifeEnd()
    {
        Die?.Invoke(this);
    }
}
