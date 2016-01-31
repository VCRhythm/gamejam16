using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class EnemySpawner : MonoBehaviour
{
    public float timeBetweenSpawns = 1f;
    public Enemy[] enemyTypes;
    SpawnPoint[] spawnPoints;

    List<Enemy> enemiesToSpawn;
    List<int> spawnCount;
    Dictionary<string, int> enemyRead = new Dictionary<string, int> { { "m", 0 }, { "t", 1 }, {"f", 2 }, {"c", 3 } };

    void Awake()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        enemiesToSpawn = new List<Enemy>();
        spawnCount = new List<int>();
    }

    public void SpawnEnemies(int night)
    {
        ReadEnemyFile(night);
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

    void ReadEnemyFile(int night)
    {
        //read wave from file
        FileInfo file = new FileInfo(Application.dataPath + "\\Night" + night + ".txt");
        StreamReader reader = file.OpenText();

        string line = reader.ReadLine();
        while (line != null)
        {
            if (enemyRead.ContainsKey(line))
            {
                enemiesToSpawn.Add(enemyTypes[enemyRead[line]]);
            }
            else
            {
                spawnCount.Add(int.Parse(line));
            }
            line = reader.ReadLine();
        }
        reader.Close();
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