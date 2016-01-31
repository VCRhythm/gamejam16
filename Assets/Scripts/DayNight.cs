using UnityEngine;

public class DayNight : MonoBehaviour {
    
    public GameObject stars;

    LightAutoIntensity lightAutoIntensity;
    float startTime;

    void Awake()
    {
        lightAutoIntensity = GetComponentInChildren<LightAutoIntensity>();
    }

    void Update()
    {
        stars.transform.rotation = transform.rotation;
    }

    public void StartDay()
    {
        lightAutoIntensity.TriggerDay();
    }

    public void StartNight()
    {
        lightAutoIntensity.TriggerNight();
    }
}