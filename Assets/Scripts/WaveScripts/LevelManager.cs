using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public float lengthOfDay = 120;
    public float numberOfNights = 2;
    public int amountOfCheeseToSpawn = 100;
    public int levelToLoadWhenFinished;
    public int thisLevel;

    public float dayTimeLeft { get { return lengthOfDay - (Time.time - dayTimeStart); } }
    public int enemiesStarted { get; private set; }
    public int enemiesRemaining { get; private set; }
    public int night { get; private set; }
    public bool isDay { get;  private set; }

    EnemySpawner enemySpawner;
    ItemSpawner itemSpawner;
    DayNight dayNight;
    AudioManager audioManager;
    float dayTimeStart;

    void Awake()
    {
        night = 0;
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
        night++;
        isDay = true;
        dayNight.StartDay();
        audioManager.playDayMusic();
        Invoke("SpawnFood", 3f);
        dayTimeStart = Time.time;
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
    }

    void StartWave()
    {
        enemiesStarted = enemySpawner.SpawnEnemies(thisLevel, night);
        enemiesRemaining = enemiesStarted;
    }
}
