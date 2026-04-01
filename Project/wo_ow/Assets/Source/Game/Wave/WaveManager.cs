using System;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    public event Action OnWaveEnd;
    public int WaveNumber;

    [SerializeField] private Spawner _spawner;
    private WaveStats _currentWaveStats;


    private void Start() {
        _currentWaveStats = new WaveStats();
        _currentWaveStats.WaveCompleteStatus = CompleteStatus.Complete;
    }

    private void Update() {
        CheckWaveStatus();
    }

    public void StartWave(WaveConfig config) {
        _currentWaveStats.AliveEnemiesCount = config.CountEnemiesOnWave;
        _currentWaveStats.AliveEnemiesList = _spawner.Spawn(config.EnemiesOnWave);
        _currentWaveStats.WaveCompleteStatus = CompleteStatus.NotComplete;
        
        ++WaveNumber;
    }

    public void DestroyAllEnemies() {
        foreach (var enemy in _currentWaveStats.AliveEnemiesList) {
            _currentWaveStats.AliveEnemiesList.Remove(enemy);
            _currentWaveStats.AliveEnemiesCount--;
            
            enemy.Die();
        }
    }

    private void CheckWaveStatus() {
        if (_currentWaveStats.WaveCompleteStatus == CompleteStatus.Complete)
            return;
        
        if (_currentWaveStats.AliveEnemiesCount <= 0) {
            _currentWaveStats.WaveCompleteStatus = CompleteStatus.Complete;
            OnWaveEnd?.Invoke();
            
            return;
        }

        CheckEachEnemyThatItsAlive();
    }

    private void CheckEachEnemyThatItsAlive() {
        for(var i = 0; i < _currentWaveStats.AliveEnemiesList.Count; ++i) {
            if (!_currentWaveStats.AliveEnemiesList[i].IsAlive) {
                --_currentWaveStats.AliveEnemiesCount;
                _currentWaveStats.AliveEnemiesList.RemoveAt(i);
            }
        }
    }
}