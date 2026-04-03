using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour {
    public float RoundTime { get; private set; } // in seconds
    public int RoundNumber;
    public WaveManager WaveManager;
    public event Action OnRoundEnd;
    public Result Result { get; private set; }

    private RoundConfig _currentRoundConfig;
    private CompleteStatus _status;

    private readonly Enemies[] _enemiesArray = {
        Enemies.EyeOfGod, Enemies.Glitch, Enemies.NeonSoldier
    };


    public void StartRound(int enemyPerRound, int enemyPerWave, int roundTime) {
        _currentRoundConfig = new RoundConfig(++RoundNumber, roundTime, enemyPerWave, GenerateEnemies(enemyPerRound));
        _currentRoundConfig.EnemyOnWaves = _currentRoundConfig.SplitEnemies();

        _status = CompleteStatus.NotComplete;

        RoundTime = roundTime;

        Result = Result.Unknown;
        
        RoundProcess();
    }
    
    private void Start() {
        WaveManager.OnWaveEnd += RoundProcess;
    }

    private void OnDisable() {
        WaveManager.OnWaveEnd -= RoundProcess;
    }

    private void Update() {
        if (_status != CompleteStatus.Complete)
            ProcessTime();
    }

    private void ProcessTime() {
        RoundTime -= Time.deltaTime;

        if (RoundTime < 0) {
            RoundTime = 0;

            WaveManager.DestroyAllEnemies();

            Result = Result.Lose;
            
            EndRound();
            
            return;
        }
    }

    private void RoundProcess() {
        if (_status == CompleteStatus.Complete)
            return;
        
        if (_currentRoundConfig.WavesCount == WaveManager.WaveNumber) {
            Result = Result.Win;

            EndRound();
            
            return;
        }
        
        WaveManager.StartWave( 
            new WaveConfig(
                _currentRoundConfig.EnemyOnWaves[WaveManager.WaveNumber],
                _currentRoundConfig.EnemyPerWave
            )
        );
    }

    private void EndRound() {
        WaveManager.WaveNumber = 0;
        _status = CompleteStatus.Complete;
            
        OnRoundEnd?.Invoke();
    }

    private List<Enemies> GenerateEnemies(int count) {
        var enemies = new List<Enemies>();

        for (var i = 0; i < count; ++i)
            // enemies[i] = _enemiesArray[Random.Range(0, 2)];
            enemies.Add(_enemiesArray[2]);

        return enemies;
    }
}
