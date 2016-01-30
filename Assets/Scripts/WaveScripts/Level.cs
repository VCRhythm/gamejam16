[System.Serializable]
public class Level {

    public Wave[] nightWaves;
    public float timeBetweenNightWaves = 30;
    int nightWaveIndex = 0;

    public Wave GetWave()
    {
        return nightWaves[nightWaveIndex++];
    }
}
