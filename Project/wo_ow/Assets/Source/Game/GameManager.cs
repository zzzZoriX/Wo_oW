using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    [SerializeField] private RoundManager _roundManager;
    [SerializeField] private GameStats _stats;
    private RoundStats[] _currentRoundsStats;
    private uint _currentRoundIndex;

    private Enemies[] _enemies = {
        Enemies.NeonSoldier, Enemies.EyeOfGod, Enemies.Glitch
    };


    private void Start() {
        _roundManager.OnRoundEnd += RoundController;
    }

    private void OnDisable() {
        _roundManager.OnRoundEnd -= RoundController;
    }

    public void StartRounds() {
        _currentRoundsStats = GenerateRounds();
        _currentRoundIndex = 0;
        
        RoundController();
    }

    private void RoundController() {
        if (_currentRoundIndex == _stats.RoundCount) {
            OnRoundsEnd();

            return;
        }

        _roundManager.StartRound(_currentRoundsStats[_currentRoundIndex++]);
    }

    private void OnRoundsEnd() {
        Debug.Log("Rounds End");
    }

    private RoundStats[] GenerateRounds() {
        var stats = new RoundStats[_stats.RoundCount];
        for (uint number = 1; number <= 5; ++number) {
            stats[number - 1] = new RoundStats(number, _stats.RoundTime, 2, 5 + number); // в будущем заменю на авто-определение кол-ва волн
            stats[number - 1].EnemyOnRound = GenerateEnemies(10 + number * 2);
        }

        return stats;
    }

    private Enemies[] GenerateEnemies(uint count) {
        var enemiesPack = new Enemies[count];

        for (uint index = 0; index < count; ++index) {
            enemiesPack[index] = _enemies[Random.Range(0, _enemies.Length)];
        }

        return enemiesPack;
    }
}