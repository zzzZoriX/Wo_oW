using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    public event Action OnWaveComplete;
    
    [SerializeField] private Spawner _spawner;
    private int _enemiesCounter;
    private List<GameObject> _enemiesAlive = new List<GameObject>();
    private bool _waveCompleted = false;


    private void Update() {
        CheckAliveEnemies();
    }

    public void SpawnWave(Enemies[] enemiesOnWave) {
        _enemiesAlive = _spawner.Spawn(enemiesOnWave);

        _enemiesCounter = enemiesOnWave.Length;

        _waveCompleted = false;
    }

    private void CheckAliveEnemies() {
        if (_enemiesAlive.Count == 0 && !_waveCompleted) {
            OnWaveComplete?.Invoke();
            _waveCompleted = true;
            return;
        }

        foreach (var enemy in _enemiesAlive) {
            if (!enemy.GetComponent<Enemy>().IsAlive) {
                _enemiesAlive.Remove(enemy);
                Destroy(enemy);
            }
        }
    }
}