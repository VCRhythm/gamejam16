using UnityEngine;

//Used to set up temporary targets for enemies
public class EnemyTarget : MonoBehaviour
{
    public Enemy targetEnemy;

    //This target only interacts with enemy layer (set in Unity's physics)
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetEnemy() == targetEnemy)
        {
            Destroy(gameObject);
        }
    }
}
