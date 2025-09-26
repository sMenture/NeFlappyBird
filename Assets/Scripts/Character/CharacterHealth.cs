using System;
using UnityEngine;
public class CharacterHealth : MonoBehaviour, IHealable, IDamageable
{
    [SerializeField] private int _value;
    private int _maxValue;

    public event Action<int> Change;

    public int Value => _value;
    public int MaxValue => _maxValue;
    public bool IsAlive => _value > 0;

    private void Awake()
    {
        _maxValue = _value;
    }

    private void Start()
    {
        Change?.Invoke(_value);
    }

    public void Heal(int healAmount)
    {
        if (healAmount < 0)
            return;

        _value = Mathf.Clamp(_value + healAmount, 0, MaxValue);

        Change?.Invoke(_value);
    }

    public void TakeDamage(int damage)
    {
        if (0 > damage)
            return;

        _value = Mathf.Clamp(_value - damage, 0, MaxValue);

        Change?.Invoke(_value);
    }
}
