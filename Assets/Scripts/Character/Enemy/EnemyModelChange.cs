using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class EnemyModelChange : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetRandomModel()
    {
        if (_sprites.Length == 0)
            return;

        int randomIndex = Random.Range(0, _sprites.Length);

        _spriteRenderer.sprite = _sprites[randomIndex];
    }
}
