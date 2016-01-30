using UnityEngine;

public class ItemSpawner : MonoBehaviour {

    public enum ItemType
    {
        Food,
        Health
    }

    public GameObject foodSpawn;
    public GameObject healthSpawn;
    public float levelXMin, levelXMax, levelZMin, levelZMax;

    public void SpawnInRandomLocation(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 position = new Vector3(Random.Range(levelXMin, levelXMax), 1, Random.Range(levelZMin, levelZMax));
            Instantiate(foodSpawn, position, Quaternion.identity);
        }
    }

    public void Spawn(ItemType type, Vector3 position)
    {
        if(type == ItemType.Food)
        {
            Instantiate(foodSpawn, position, Quaternion.identity);
        }
        else if(type == ItemType.Health)
        {
            Instantiate(healthSpawn, position, Quaternion.identity);
        }
    }
}
