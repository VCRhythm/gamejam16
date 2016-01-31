using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public float lengthOfDay = 120;
    public float numberOfNights = 2;
    public int amountOfCheeseToSpawn = 100;
    public int levelToLoadWhenFinished;

    int night = 1;

    EnemySpawner enemySpawner;
    ItemSpawner itemSpawner;
    DayNight dayNight;
    AudioManager audioManager;

    void Awake()
    {
        dayNight = FindObjectOfType<DayNight>();
        enemySpawner = GetComponent<EnemySpawner>();
        itemSpawner = GetComponent<ItemSpawner>();
        audioManager = GetComponent<AudioManager>();
    }

    void Start()
    {        
        StartDay();
    }

    public void StartDay()
    {
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
        dayNight.StartNight();
        audioManager.playNightMusic();
        Invoke("StartWave", 3f);
        night++;
    }

    void StartWave()
    {
        enemySpawner.SpawnEnemies(night);
    }

}
