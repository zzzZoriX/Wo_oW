using System;
using UnityEngine;

public class RoundManager : MonoBehaviour {
    public event Action OnRoundEnd;
    public RoundStats CurrentRoundStats;
    
    [SerializeField] private WaveManager _waveManager;
    private Enemies[][] _enemiesOnWaves;
    private uint _currentWavesCount = 0;
    private bool _roundEnd;


    private void Start() {
        _waveManager.OnWaveComplete += WaveController;
    }

    private void OnDisable() {
        _waveManager.OnWaveComplete -= WaveController;
    }

    public bool StartRound(RoundStats roundStats) {
        _enemiesOnWaves = SplitEnemies(roundStats.EnemyOnRound, roundStats.EnemyPerWave);
        CurrentRoundStats = roundStats;
        _roundEnd = false;
        
        return true;
    }

    private void WaveController() {
        if (_currentWavesCount == CurrentRoundStats.WavesCount && !_roundEnd) {
            OnRoundEnd?.Invoke();
            _roundEnd = true;
            return;
        }
        
        _waveManager.SpawnWave(_enemiesOnWaves[_currentWavesCount++]);
    }

    private Enemies[][] SplitEnemies(Enemies[] allEnemies, uint enemyPerWave) {
        var wavesCount = allEnemies.Length / enemyPerWave;
        var splitedEnemies = new Enemies[wavesCount][];
        var j = 0;

        for (var i = 0; i < wavesCount; ++i) {
            for (; j < enemyPerWave; ++j)
                splitedEnemies[i][j] = allEnemies[j];
        }

        return splitedEnemies;
    }
}
