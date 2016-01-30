using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public int spawnCount = 1;
    public float timeBetweenSpawns = 1f;
    SpawnPoint[] spawnPoints;
    Enemy[] enemiesToSpawn;

    void Awake()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
    }

    public void SpawnEnemies(Enemy[] enemies, int count)
    {
        enemiesToSpawn = enemies;
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
        spawnPoint.Spawn(GetEnemy());
    }

    SpawnPoint GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    Enemy GetEnemy()
    {
        return enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
    }
}
