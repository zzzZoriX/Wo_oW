using System.Collections.Generic;

public class WaveConfig {
    public List<Enemies> EnemiesOnWave;
    public int CountEnemiesOnWave;

    public WaveConfig(List<Enemies> enemiesOnWave, int countEnemiesOnWave) {
        EnemiesOnWave = enemiesOnWave;
        CountEnemiesOnWave = countEnemiesOnWave;
    }
}