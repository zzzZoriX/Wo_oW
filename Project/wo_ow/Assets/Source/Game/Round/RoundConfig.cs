using System.Collections.Generic;

public class RoundConfig {
    public int RoundNumber; // номер раунда
    public float RoundTime; // время на раунд
    public List<Enemies> EnemyOnRound; // все враги на раунд
    public List<List<Enemies>> EnemyOnWaves;
    public int EnemyPerWave; // кол-во спавнящихся за раз врагов
    public int WavesCount;

    public RoundConfig(int roundNumber, float roundTime, int enemyPerWave, List<Enemies> enemiesList) {
        RoundNumber = roundNumber;
        RoundTime = roundTime;
        EnemyPerWave = enemyPerWave;
        EnemyOnRound = enemiesList;
        WavesCount = enemiesList.Count / EnemyPerWave;
    }
    
    public List<List<Enemies>> SplitEnemies() {
        var enemiesOnWave = new List<List<Enemies>>();

        for (var i = 0; i < EnemyOnRound.Count; ++i) {
            var enemyOnWave = new List<Enemies>();
            
            for (var j = 0; j < EnemyPerWave; ++j)
                enemyOnWave.Add(EnemyOnRound[i]);
            
            enemiesOnWave.Add(enemyOnWave);
        }

        return enemiesOnWave;
    }

}