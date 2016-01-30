using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public int spawnCount = 1;
    public float timeBetweenSpawns = 1f;
    SpawnPoint[] spawnPoints;

    void Start()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        SpawnEnemies(spawnCount);
    }

    void SpawnEnemies(int count)
    {
        StartCoroutine(StartSpawning(count));
    }

    IEnumerator StartSpawning(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    void SpawnEnemy()
    {
        SpawnPoint spawnPoint = GetSpawnPoint();
        spawnPoint.Spawn();
    }

    SpawnPoint GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }    
}
