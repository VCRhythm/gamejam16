using UnityEngine;
using System.Collections.Generic;

public class FoodSpawner : MonoBehaviour {

    public GameObject foodSpawn;
    public float levelXMin, levelXMax, levelZMin, levelZMax;

    List<Vector3> spawnLocations = new List<Vector3>();

    public void Spawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 position = new Vector3(Random.Range(levelXMin, levelXMax), 0, Random.Range(levelZMin, levelZMax));
            Instantiate(foodSpawn, position, Quaternion.identity);
        }
    }
}
