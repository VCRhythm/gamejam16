[System.Serializable]
public class Level {

    public Wave[] nightWaves;
    public float timeBetweenNightWaves = 30;
    public int amountOfDayFood;
    int nightWaveIndex = 0;

    public Wave GetWave()
    {
        return nightWaves[nightWaveIndex++];
    }
}
