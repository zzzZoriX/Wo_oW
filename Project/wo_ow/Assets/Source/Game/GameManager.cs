using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private Spawner spawner;
    [SerializeField] private RoundManager roundManager;

    private void InitializeComponents() {
        roundManager.Construct(spawner);
    }
    
    [SerializeField] private GameObject player;
    private CompleteStatus _gameStatus;
    private GameConfig _config;
    private GameStats _stats;


    private void Start() {
        _config = DeserializeData.Deserialize<GameConfig>("./Assets/Source/Data/GameConfig.json");
        
        InitializeComponents();
        roundManager.OnRoundEnd += ProcessGame;

        _stats.EnemyPerRound = _config.DefaultEnemyPerRoundCount;
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
        
        roundManager.StartRound(_stats.EnemyPerRound);
        
        _stats.IncreaseEnemiesOnRound(_config.EPRIncreaseFactor);
    }

    /// <summary>
    /// invoke when player complete all rounds and teleport him to the lobby
    /// </summary>
    private void EndGame() {
        player.transform.position = new Vector3(); // <- set value to the lobby coordinates
    }
}