[System.Serializable]
public class Level {

    public Wave[] waves;
    public float timeBetweenWaves = 30;
    int waveIndex = 0;

    public Wave GetWave()
    {
        return waves[waveIndex++];
    }
}
