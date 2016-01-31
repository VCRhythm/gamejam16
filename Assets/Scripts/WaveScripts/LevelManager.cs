﻿using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class LevelManager : MonoBehaviour {

    public float lengthOfDay = 120;
    public float numberOfNights = 2;
    public int amountOfCheeseToSpawn = 100;

    int night = 1;
    EnemySpawner enemySpawner;
    ItemSpawner itemSpawner;
    DayNight dayNight;

    void Awake()
    {
        dayNight = FindObjectOfType<DayNight>();
        enemySpawner = GetComponent<EnemySpawner>();
        itemSpawner = GetComponent<ItemSpawner>();
    }

    void Start()
    {
        StartDay();
    }

    public void StartDay()
    {
        dayNight.StartDay();

        Invoke("SpawnFood", 3f);
        Invoke("StartNight", lengthOfDay);
    }

    void SpawnFood()
    {
        itemSpawner.SpawnInRandomLocation(amountOfCheeseToSpawn);
    }

    void StartNight()
    {
        dayNight.StartNight();
       
        Invoke("StartWave", 3f);
    }

    void StartWave()
    {
        enemySpawner.SpawnEnemies(night);
    }

}