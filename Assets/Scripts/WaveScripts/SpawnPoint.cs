using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Enemy[] enemyTypes;

    public void Spawn(Enemy toSpawn)
    {
        Instantiate(toSpawn, transform.position, Quaternion.identity);
    }
}