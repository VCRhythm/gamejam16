using UnityEngine;
using System.Collections.Generic;

public static class TransformExtensions {

    private static Dictionary<Transform, Player> playerScriptLookupFromTransform = new Dictionary<Transform, Player>();
    private static Dictionary<Collider, Enemy> enemyScriptLookupFromCollider = new Dictionary<Collider, Enemy>();
    private static Dictionary<Collider, IDamageable> iDamageableLookupFromCollider = new Dictionary<Collider, IDamageable>();
    private static Dictionary<Collider, Food> foodScriptLookupFromCollider = new Dictionary<Collider, Food>();
    private static Dictionary<Transform, Creature> creatureScriptLookupFromTransform = new Dictionary<Transform, Creature>();

    public static void Register(this Transform trans)
    {
        Food foodScript = trans.GetComponent<Food>();
        Creature creatureScript = trans.GetComponent<Creature>();

        Collider collider = trans.GetComponentInChildren<Collider>();
        IDamageable iDamageable = (IDamageable)trans.GetComponent(typeof(IDamageable));

        if (creatureScript)
        {
            Player playerScript = trans.GetComponent<Player>();
            Enemy enemyScript = trans.GetComponent<Enemy>();

            if (playerScript)
            {
                playerScriptLookupFromTransform.Add(trans, playerScript);
            }
            else if (enemyScript)
            {
                enemyScriptLookupFromCollider.Add(collider, enemyScript);
            }
        }
        else if(foodScript)
        {
            foodScriptLookupFromCollider.Add(collider, foodScript);
        }

        if(iDamageable != null)
        {
            iDamageableLookupFromCollider.Add(collider, iDamageable);
        }
    }

    public static Food GetFood(this Collider c)
    {
        return foodScriptLookupFromCollider[c];
    }

    public static IDamageable GetIDamageable(this Collider c)
    {
        return iDamageableLookupFromCollider[c];
    }

    public static Enemy GetEnemy(this Collider c)
    {
        return enemyScriptLookupFromCollider[c];
    }

    public static Player GetPlayer(this Transform t)
    {
        return playerScriptLookupFromTransform[t];
    }

    public static Creature GetCreature(this Transform t)
    {
        return creatureScriptLookupFromTransform[t];
    }

}
