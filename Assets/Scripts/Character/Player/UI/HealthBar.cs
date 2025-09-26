using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthPoint[] _healthPoints;
    [SerializeField] private CharacterHealth _health;

    private void OnEnable()
    {
        _health.Change += UpdateUI;
    }

    private void OnDisable()
    {
        _health.Change -= UpdateUI;
    }

    private void UpdateUI(int value)
    {
        int damageTaken = _health.MaxValue - value;
        var healthyPoints = _healthPoints.Skip(damageTaken);

        foreach (var point in _healthPoints)
            point.SetDisable();

        foreach (var point in healthyPoints)
            point.SetEnable();
    }
}

[System.Serializable]
public class HealthPoint
{
    [SerializeField] private Image _point;

    public Image Point => _point;

    public void SetEnable()
    {
        _point.enabled = true;
    }

    public void SetDisable()
    {
        _point.enabled = false;
    }
}
