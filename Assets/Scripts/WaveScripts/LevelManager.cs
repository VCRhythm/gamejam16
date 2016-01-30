using UnityEngine;

public class LevelManager : MonoBehaviour {

    public Level[] levels;

    int levelIndex = 0;
    Level level;
    EnemySpawner enemySpawner;
    DayNight dayNight;

    void Awake()
    {
        dayNight = FindObjectOfType<DayNight>();
        enemySpawner = GetComponent<EnemySpawner>();
    }

    public void StartLevelNight()
    {
        level = levels[levelIndex++];
        
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
            dayNight.StartDay();
        }
    }
}
