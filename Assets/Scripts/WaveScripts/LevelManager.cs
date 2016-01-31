using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public float lengthOfDay = 120;
    public float numberOfNights = 2;
    public int amountOfCheeseToSpawn = 100;
    public int levelToLoadWhenFinished;

    public int enemiesStarted { get; private set; }
    public int enemiesRemaining { get; private set; }
    public int night { get; private set; }
    public bool isDay { get;  private set; }

    EnemySpawner enemySpawner;
    ItemSpawner itemSpawner;
    DayNight dayNight;
    AudioManager audioManager;

    void Awake()
    {
        night = 1;
        dayNight = FindObjectOfType<DayNight>();
        enemySpawner = GetComponent<EnemySpawner>();
        itemSpawner = GetComponent<ItemSpawner>();
        audioManager = GetComponent<AudioManager>();
    }

    void Start()
    {        
        StartDay();
    }

    public void ReportEnemyDeath()
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            StartDay();
            enemiesStarted = 0;
        }
    }

    public void StartDay()
    {
        isDay = true;
        dayNight.StartDay();
        audioManager.playDayMusic();
        Invoke("SpawnFood", 3f);
        Invoke("StartNight", lengthOfDay);
    }

    void SpawnFood()
    {
        if(night > numberOfNights)
        {
            SceneManager.LoadScene(levelToLoadWhenFinished);
        }

        itemSpawner.SpawnInRandomLocation(amountOfCheeseToSpawn);
    }

    void StartNight()
    {
        isDay = false;
        dayNight.StartNight();
        audioManager.playNightMusic();
        Invoke("StartWave", 3f);
        night++;
    }

    void StartWave()
    {
        enemiesStarted = enemySpawner.SpawnEnemies(night);
        enemiesRemaining = enemiesStarted;
    }

}
