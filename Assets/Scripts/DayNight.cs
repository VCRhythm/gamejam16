using UnityEngine;

public class DayNight : MonoBehaviour {
    
    public GameObject stars;
    public bool isDay { get; private set; }

    LightAutoIntensity lightAutoIntensity;
    float startTime;
    float dayTime = 120;
    float nightTime = 120;

    void Awake()
    {
        lightAutoIntensity = GetComponentInChildren<LightAutoIntensity>();
    }

    void Update()
    {
        stars.transform.rotation = transform.rotation;
    }

    void StartDay()
    {
        isDay = true;
        lightAutoIntensity.TransitionToDay();
        startTime = Time.time;

        Invoke("StartNight", dayTime);
    }

    void StartNight()
    {
        isDay = false;
        lightAutoIntensity.TransitionToNight();
        startTime = Time.time;

        Invoke("StartDay", nightTime);
    }
}