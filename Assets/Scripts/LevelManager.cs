using UnityEngine;

public class LevelManager : MonoBehaviour {

    public Level[] levels;

    int levelIndex = 0;
    Level level;
    EnemySpawner enemySpawner;

    void Awake()
    {
        enemySpawner = GetComponent<EnemySpawner>();
    }

    void Start()
    {
        StartLevel();
    }

    void StartLevel()
    {
        level = levels[levelIndex++];
        
        StartWave();
        Invoke("StartWave", level.timeBetweenWaves);
    }

    void StartWave()
    {
        Wave wave = level.GetWave();
        enemySpawner.SpawnEnemies(wave.enemyTypes, wave.enemyCount);
    }
}
