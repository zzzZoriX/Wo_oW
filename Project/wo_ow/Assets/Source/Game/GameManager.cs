using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private GameObject player;
    private CompleteStatus _gameStatus;
    private GameConfig _config;
    private EPR _epr;


    public GameStats GetGameStats()
        => new(roundManager.RoundNumber, roundManager.WaveManager.WaveNumber);
    
    private void Start() {
        _epr = new EPR();
        
        _config = DeserializeData.Deserialize<GameConfig>("./Assets/Source/Data/GameConfig.json");
        
        roundManager.OnRoundEnd += ProcessGame;

        _epr.EnemyPerRound = _config.DefaultEnemyPerRoundCount;
    }

    private void OnDisable() {
        roundManager.OnRoundEnd -= ProcessGame;
    }
    
    public void StartGame() {
        roundManager.RoundNumber = 0;
        
        _gameStatus = CompleteStatus.NotComplete;
        
        ProcessGame();
    }

    private void ProcessGame() {
        if (_gameStatus == CompleteStatus.Complete)
            return;

        if (roundManager.RoundNumber == _config.RoundCount) {
            _gameStatus = CompleteStatus.Complete;

            EndGame();

            return;
        }

        roundManager.StartRound(_epr.EnemyPerRound, _epr.EnemyPerRound / _config.SplitFactor);
        
        _epr.IncreaseEnemiesOnRound(_config.EPRIncreaseValue);
    }

    /// <summary>
    /// invoke when player complete all rounds and teleport him to the lobby
    /// </summary>
    private void EndGame() {
        player.transform.position = new Vector3(); // <- set value to the lobby coordinates
    }
}