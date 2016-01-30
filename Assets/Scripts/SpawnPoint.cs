using UnityEngine;

public class SpawnPoint : MonoBehaviour {
    public Enemy[] enemyTypes;
    
    public void Spawn()
    {
        Enemy enemy = GetEnemy();
        Instantiate(enemy, transform.position, Quaternion.identity);
    }

    Enemy GetEnemy()
    {
        return enemyTypes[Random.Range(0, enemyTypes.Length)];
    }
}
