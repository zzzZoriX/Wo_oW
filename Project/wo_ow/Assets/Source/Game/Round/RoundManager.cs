using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour {
    public event Action OnRoundEnd;
    public int RoundNumber;
    public int RoundTime; // in seconds

    [SerializeField] private WaveManager _waveManager;
    private RoundConfig _currentRoundConfig;
    private CompleteStatus _status;

    private readonly Enemies[] _enemiesArray = {
        Enemies.EyeOfGod, Enemies.Glitch, Enemies.NeonSoldier
    };


    private void Start() {
        _waveManager.OnWaveEnd += RoundProcess;
    }

    private void OnDisable() {
        _waveManager.OnWaveEnd -= RoundProcess;
    }

    public void StartRound(int enemyPerRound) {
        _currentRoundConfig = new RoundConfig(++RoundNumber, RoundTime, enemyPerRound, GenerateEnemies(enemyPerRound));
        _currentRoundConfig.EnemyOnWaves = _currentRoundConfig.SplitEnemies();

        _status = CompleteStatus.NotComplete;
        
        RoundProcess();
    }

    private void RoundProcess() {
        if (_status == CompleteStatus.Complete)
            return;
        
        if (_currentRoundConfig.WavesCount == _waveManager.WaveNumber) {
            _waveManager.WaveNumber = 0;
            _status = CompleteStatus.Complete;
            
            OnRoundEnd?.Invoke();
            
            return;
        }
        
        _waveManager.StartWave( 
            new WaveConfig(
                _currentRoundConfig.EnemyOnWaves[_waveManager.WaveNumber],
                _currentRoundConfig.EnemyPerWave
            )
        );
    }

    private List<Enemies> GenerateEnemies(int count) {
        var enemies = new List<Enemies>();

        for (var i = 0; i < count; ++i)
            // enemies[i] = _enemiesArray[Random.Range(0, 2)];
            enemies.Add(_enemiesArray[2]);

        return enemies;
    }
}
