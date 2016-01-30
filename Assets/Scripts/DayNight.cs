using UnityEngine;

public class DayNight : MonoBehaviour {
    
    public GameObject stars;
    public float lengthOfDay = 120;
    public bool isDay { get; private set; }

    LevelManager levelManager;
    LightAutoIntensity lightAutoIntensity;
    float startTime;

    void Awake()
    {
        lightAutoIntensity = GetComponentInChildren<LightAutoIntensity>();
    }

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        StartDay();
    }

    void Update()
    {
        stars.transform.rotation = transform.rotation;
    }

    public void StartDay()
    {
        isDay = true;
        lightAutoIntensity.TriggerDay();
        startTime = Time.time;

        Invoke("StartNight", lengthOfDay);
    }

    void StartNight()
    {
        isDay = false;
        lightAutoIntensity.TriggerNight();
        startTime = Time.time;
        Invoke("StartNightLevel", 3f);
    }

    void StartNightLevel()
    {
        levelManager.StartLevelNight();
    }
}