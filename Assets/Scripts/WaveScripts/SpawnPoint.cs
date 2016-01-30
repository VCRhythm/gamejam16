using UnityEngine;

public class SpawnPoint : MonoBehaviour {    
    public void Spawn(Enemy enemy)
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
