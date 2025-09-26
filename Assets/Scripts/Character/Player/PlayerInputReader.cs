using System;
using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    private readonly string Horizontal = nameof(Horizontal);
    private readonly string Vertical = nameof(Vertical);
    private readonly KeyCode AttackButton = KeyCode.Mouse0;

    public event Action<Vector2> InputDirection;
    public event Action AttackDown;

    private void Update()
    {
        float vertical = Input.GetAxis(Vertical);
        float horizontal = Input.GetAxis(Horizontal);

        InputDirection?.Invoke(new Vector2(horizontal, vertical));

        if (Input.GetKey(AttackButton))
            AttackDown?.Invoke();
    }
}
