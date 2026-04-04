public class GameStats {
    public int WaveNumber;
    public int RoundNumber;
    public float RoundTime;
    public Result RoundResult;

    public GameStats(int roundNumber, int waveNumber, float roundTime, Result result) {
        WaveNumber = waveNumber;
        RoundNumber = roundNumber;
        RoundTime = roundTime;
        RoundResult = result;
    }
}