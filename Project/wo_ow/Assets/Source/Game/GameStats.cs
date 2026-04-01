public class GameStats {
    public int WaveNumber;
    public int RoundNumber;
    public float RoundTime;

    public GameStats(int roundNumber, int waveNumber, float roundTime) {
        WaveNumber = waveNumber;
        RoundNumber = roundNumber;
        RoundTime = roundTime;
    }
}