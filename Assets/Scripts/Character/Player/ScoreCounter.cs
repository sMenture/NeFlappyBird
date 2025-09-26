using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private const string ScoreLabel = "Score";

    [SerializeField] private TextMeshProUGUI _scoreText;

    private float _timeHasPassed = 0;

    private void Update()
    {
        _timeHasPassed += Time.deltaTime;

        _scoreText.text = $"{ScoreLabel}: {(int)_timeHasPassed + 1}";
    }

    public void Reset()
    {
        _timeHasPassed = 0;
    }
}
