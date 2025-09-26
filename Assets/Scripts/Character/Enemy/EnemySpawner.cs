using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private float _currentTime = 0;
    private List<int> _selectedIndexSpawnPoint = new List<int>();
    private List<Enemy> _enemies = new List<Enemy>();


    private void Update()
    {
        _timeHasPassed += Time.deltaTime;
        _currentTime -= Time.deltaTime;

        if (0 > _currentTime)
            SpawnNewWaves();
    }

    private void SpawnNewWaves()
    {
        _selectedIndexSpawnPoint.Clear();
        _currentTime = _timeBetweenWaves;

        int enemiesPerWave = (int)Mathf.Clamp(_timeHasPassed / _enemiesPerWave, _minSpawnEnemy, _targetPoints.Length);
        Debug.Log(enemiesPerWave);

        for (int i = 0; i < enemiesPerWave; i++)
        {
            if (_enemyPool.CanReturnDequeueElememt == false)
                return;

            Enemy enemy = _enemyPool.GiveElement();
            enemy.Attack.SetBulletPool(_bulletPool);
            enemy.Die += DieEnemy;

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
        enemy.Die -= DieEnemy;

        _enemyPool.ReturnToPool(enemy);
    }

    public void Reset()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Attack.Reset();
            _enemyPool.ReturnToPool(enemy);
        }

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
