using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _horizontalRotation = 30;
    [SerializeField] private Vector2 _VerticalRotation = new Vector2(40, 40);

    private Quaternion _basicRotation;

    private void Awake()
    {
        _basicRotation = transform.rotation;
    }

    public void Rotation(Vector2 inputDirection)
    {
        float yInput = inputDirection.y;
        float xInput = inputDirection.x;

        Vector3 targetRotation = Vector3.zero;

        targetRotation.x = xInput * _horizontalRotation;
        targetRotation.y = yInput * _VerticalRotation.y;
        targetRotation.z = yInput * _VerticalRotation.x;

        LerpRotation(targetRotation);
    }

    private void LerpRotation(Vector3 targetEuler)
    {
        Quaternion targetRotation = _basicRotation * Quaternion.Euler(targetEuler);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _speed * Time.deltaTime);
    }
}