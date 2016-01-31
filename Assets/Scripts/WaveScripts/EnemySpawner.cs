using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class EnemySpawner : MonoBehaviour
{

    // public int spawnCount = 1;
    public float timeBetweenSpawns = 1f;
    public Enemy[] enemyTypes;
    SpawnPoint[] spawnPoints;
    List<Enemy> enemiesToSpawn;
    List<int> spawnCount;

    int night = 0;

    string line = " ";
    StreamReader reader;
    FileInfo file;

    void Awake()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        enemiesToSpawn = new List<Enemy>();
        spawnCount = new List<int>();
    }

    void OnEnable()
    {
        //read wave from file
        file = new FileInfo(Application.dataPath + "\\Night" + night + ".txt");
        reader = file.OpenText();
        line = reader.ReadLine();
        while (line != null)
        {
            if (line == "E")
            {
                enemiesToSpawn.Add(enemyTypes[0]);
            }
            else
            {
                spawnCount.Add(int.Parse(line));
            }
            line = reader.ReadLine();
        }
        SpawnEnemies();
        reader.Close();
    }

    void OnDisable()
    {
        night++;
    }

    public void SpawnEnemies()
    {
        StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            for (int j = 0; j < spawnCount[i]; j++)
            {
                SpawnEnemy(enemiesToSpawn[i]);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }

    void SpawnEnemy(Enemy toSpawn)
    {
        SpawnPoint spawnPoint = GetSpawnPoint();
        spawnPoint.Spawn(toSpawn);
    }

    SpawnPoint GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}