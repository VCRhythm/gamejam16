using UnityEngine;

public class LevelManager : MonoBehaviour {

    public float lengthOfDay = 120;
    public Level[] levels;

    int levelIndex = 0;
    Level level;
    EnemySpawner enemySpawner;
    FoodSpawner foodSpawner;
    DayNight dayNight;

    void Awake()
    {
        dayNight = FindObjectOfType<DayNight>();
        enemySpawner = GetComponent<EnemySpawner>();
        foodSpawner = GetComponent<FoodSpawner>();
    }

    void Start()
    {
        StartDay();
    }

    void StartDay()
    {
        dayNight.StartDay();

        level = levels[levelIndex++];
        foodSpawner.Spawn(level.amountOfDayFood);

        Invoke("StartNight", lengthOfDay);
    }

    void StartNight()
    {
        dayNight.StartNight();
        
        StartWave();
        Invoke("StartWave", level.timeBetweenNightWaves);
    }

    void StartWave()
    {
        Wave wave = level.GetWave();

        if (wave != null)
        {
            enemySpawner.SpawnEnemies(wave.enemyTypes, wave.enemyCount);
        }
        else
        {
            StartDay();
        }
    }
}
