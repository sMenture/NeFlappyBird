using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    [Header("")]
    [SerializeField] private Player _player;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private EnemyPool _enemyPool;

    private void Start()
    {
        EndGame();
    }

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
        _player.Die += EndGame;
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartGame);
        _player.Die -= EndGame;
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        _startButton.gameObject.SetActive(true);
    }

    private void StartGame()
    {
        Time.timeScale = 1.0f;
        _startButton.gameObject.SetActive(false);

        _enemyPool.Reset();
        _bulletPool.Reset();
        _enemySpawner.Reset();
        _scoreCounter.Reset();
        _player.Reset();
    }
}
