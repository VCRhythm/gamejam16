using UnityEngine;
using System.Collections;

public class DayNight : MonoBehaviour {
    

    Material sky;
    public GameObject stars;
    void Start()
    {
        sky = RenderSettings.skybox;
    }

    bool lighton = false;

    // Update is called once per frame
    void Update()
    {
        stars.transform.rotation = transform.rotation;
    }
}