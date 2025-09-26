using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(PlayerRotation))]
[RequireComponent(typeof(CharacterHealth))]
[RequireComponent(typeof(CharacterAttacker))]
[RequireComponent(typeof(PlayerInputReader))]
public class Player : MonoBehaviour
{
    private CharacterAttacker _attacker;
    private PlayerInputReader _inputReader;
    private CharacterHealth _health;
    private CharacterMover _mover;
    private PlayerRotation _rotation;

    public event Action Die;

    private void Awake()
    {
        _attacker = GetComponent<CharacterAttacker>();
        _inputReader = GetComponent<PlayerInputReader>();
        _health = GetComponent<CharacterHealth>();
        _mover = GetComponent<CharacterMover>();
        _rotation = GetComponent<PlayerRotation>();
    }

    private void OnEnable()
    {
        _inputReader.AttackDown += _attacker.Attack;
        _inputReader.InputDirection += _mover.Move;
        _inputReader.InputDirection += _rotation.Rotation;

        _health.Change += HealthChange;
    }

    private void OnDisable()
    {
        _inputReader.AttackDown -= _attacker.Attack;
        _inputReader.InputDirection -= _mover.Move;
        _inputReader.InputDirection -= _rotation.Rotation;

        _health.Change -= HealthChange;
    }

    private void HealthChange(int value)
    {
        if (_health.IsAlive)
            return;

        Die?.Invoke();
    }

    public void Reset()
    {
        _attacker.Reset();
        _health.Heal(_health.MaxValue);
        transform.position = Vector2.zero;
    }
}
