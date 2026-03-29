using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour {
    public event Action OnRoundEnd;
    public int RoundNumber;
    public int RoundTime; // in seconds
    public WaveManager WaveManager;

    private RoundConfig _currentRoundConfig;
    private CompleteStatus _status;

    private readonly Enemies[] _enemiesArray = {
        Enemies.EyeOfGod, Enemies.Glitch, Enemies.NeonSoldier
    };


    private void Start() {
        WaveManager.OnWaveEnd += RoundProcess;
    }

    private void OnDisable() {
        WaveManager.OnWaveEnd -= RoundProcess;
    }

    public void StartRound(int enemyPerRound, int enemyPerWave) {
        _currentRoundConfig = new RoundConfig(++RoundNumber, RoundTime, enemyPerWave, GenerateEnemies(enemyPerRound));
        _currentRoundConfig.EnemyOnWaves = _currentRoundConfig.SplitEnemies();

        _status = CompleteStatus.NotComplete;
        
        RoundProcess();
    }

    private void RoundProcess() {
        if (_status == CompleteStatus.Complete)
            return;
        
        if (_currentRoundConfig.WavesCount == WaveManager.WaveNumber) {
            WaveManager.WaveNumber = 0;
            _status = CompleteStatus.Complete;
            
            OnRoundEnd?.Invoke();
            
            return;
        }
        
        WaveManager.StartWave( 
            new WaveConfig(
                _currentRoundConfig.EnemyOnWaves[WaveManager.WaveNumber],
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
