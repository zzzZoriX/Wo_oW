using System.Collections.Generic;
using System.Linq;

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
        return EnemyOnRound
            .Select((enemy, index) => new { enemy, index })
            .GroupBy(x => x.index / EnemyPerWave)
            .Select(group => group.Select(x => x.enemy).ToList())
            .ToList();
    }

}