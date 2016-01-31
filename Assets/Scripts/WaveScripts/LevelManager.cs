using UnityEngine;

public class LevelManager : MonoBehaviour {

    public float lengthOfDay = 120;
    public Level[] levels;

    int levelIndex = 0;
    Level level;
    EnemySpawner enemySpawner;
    ItemSpawner foodSpawner;
    DayNight dayNight;

    void Awake()
    {
        dayNight = FindObjectOfType<DayNight>();
        enemySpawner = GetComponent<EnemySpawner>();
        foodSpawner = GetComponent<ItemSpawner>();
    }

    void StartDay()
    {
        dayNight.StartDay();

        level = levels[levelIndex++];
        Invoke("SpawnFood", 3f);

        Invoke("StartNight", lengthOfDay);
    }

    void SpawnFood()
    {
        foodSpawner.SpawnInRandomLocation(level.amountOfDayFood);
    }

    void StartNight()
    {
        dayNight.StartNight();

        Invoke("StartWave", 3f);
        Invoke("StartWave", level.timeBetweenNightWaves);
    }

    void StartWave()
    {
        Wave wave = level.GetWave();

        if (wave != null)
        {
            //enemySpawner.SpawnEnemies(wave.enemyTypes, wave.enemyCount);
        }
        else
        {
            StartDay();
        }
    }
}
