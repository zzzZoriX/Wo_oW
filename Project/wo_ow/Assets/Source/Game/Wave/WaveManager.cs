using System;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    public event Action OnWaveEnd;
    public int WaveNumber;

    private Spawner _spawner;
    private WaveStats _currentWaveStats;

    private void Update() {
        CheckWaveStatus();
    }

    public void Construct(Spawner spawner) {
        _spawner = spawner;
        _currentWaveStats = new WaveStats();
        WaveNumber = 0;
    }

    public void GenerateWave(WaveConfig config) {
        _currentWaveStats.AliveEnemiesCount = config.CountEnemiesOnWave;
        _currentWaveStats.AliveEnemiesList = _spawner.Spawn(config.EnemiesOnWave);
        _currentWaveStats.WaveCompleteStatus = CompleteStatus.NotComplete;
        
        ++WaveNumber;
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
        foreach (var enemy in _currentWaveStats.AliveEnemiesList) {
            if (!enemy.IsAlive) {
                --_currentWaveStats.AliveEnemiesCount;
                _currentWaveStats.AliveEnemiesList.Remove(enemy);
            }
        }
    }
}