public class RoundStats
{
    public uint RoundNumber;        // номер раунда
    public float RoundTime;         // время на раунд
    public Enemies[] EnemyOnRound;  // все враги на раунд
    public uint EnemyPerWave;       // кол-во спавнящихся за раз врагов
    public uint WavesCount;

    public RoundStats(uint roundNumber, float roundTime, uint wavesCount, uint enemyPerWave) {
        RoundNumber = roundNumber;
        RoundTime = roundTime;
        EnemyPerWave = enemyPerWave;
        WavesCount = wavesCount;
    }
}