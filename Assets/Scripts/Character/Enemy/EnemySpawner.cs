using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private Transform[] _targetPoints;

    [Header("Settings")]
    [SerializeField] private float _timeBetweenWaves = 3;
    [SerializeField] private int _enemiesPerWave;

    private int _minSpawnEnemy = 2;
    private float _timeHasPassed = 0;
    private List<int> _selectedIndexSpawnPoint = new List<int>();
    private List<Enemy> _enemies = new List<Enemy>();
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(SpawnNewWaves());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }


    private void Update()
    {
        _timeHasPassed += Time.deltaTime;
    }

    private IEnumerator SpawnNewWaves()
    {
        yield return _timeBetweenWaves;

        _selectedIndexSpawnPoint.Clear();

        int enemiesPerWave = (int)Mathf.Clamp(_timeHasPassed / _enemiesPerWave, _minSpawnEnemy, _targetPoints.Length);
        Debug.Log(enemiesPerWave);

        for (int i = 0; i < enemiesPerWave; i++)
        {
            if (_enemyPool.HasElements == false)
                yield return null;

            Enemy enemy = _enemyPool.GiveElement();
            enemy.Attack.Initialize(_bulletPool);
            enemy.OnEnemyDied += DieEnemy;

            _enemies.Add(enemy);

            Vector2 spawnPoint = GiveRandomSpawnPosition();
            enemy.transform.position = spawnPoint;
        }
    }

    private Vector2 GiveRandomSpawnPosition()
    {
        if (_targetPoints.Length <= _selectedIndexSpawnPoint.Count)
            return Vector2.zero;

        int randomIndex = 0;

        do
        {
            randomIndex = Random.Range(0, _targetPoints.Length);
        } while (_selectedIndexSpawnPoint.Contains(randomIndex));

        _selectedIndexSpawnPoint.Add(randomIndex);

        return _targetPoints[randomIndex].position;
    }

    private void DieEnemy(Enemy enemy)
    {
        enemy.OnEnemyDied -= DieEnemy;

        _enemyPool.ReturnToPool(enemy);
    }

    public void Reset()
    {
        _timeHasPassed = 0;
        _currentTime = 0;
        _selectedIndexSpawnPoint.Clear();
        _enemies.Clear();
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _targetPoints = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
            _targetPoints[i] = transform.GetChild(i);
    }
    #endif
}
