using UnityEngine;
using System.Collections.Generic;

public static class TransformExtensions {

    private static Dictionary<Transform, Player> playerScriptLookupFromTransform = new Dictionary<Transform, Player>();
    private static Dictionary<Transform, Enemy> enemyScriptLookupFromTransform = new Dictionary<Transform, Enemy>();
    private static Dictionary<Collider, Player> playerScriptLookupFromCollider = new Dictionary<Collider, Player>();
    private static Dictionary<Collider, Enemy> enemyScriptLookupFromCollider = new Dictionary<Collider, Enemy>();

    public static void Register(this Transform trans)
    {
        Player playerScript = trans.GetComponent<Player>();
        Enemy enemyScript = trans.GetComponent<Enemy>();
        Collider collider = trans.GetComponentInChildren<Collider>();

        if(playerScript)
        {
            playerScriptLookupFromTransform.Add(trans, playerScript);
            playerScriptLookupFromCollider.Add(collider, playerScript);
        }
        else if(enemyScript)
        {
            enemyScriptLookupFromTransform.Add(trans, enemyScript);
            enemyScriptLookupFromCollider.Add(collider, enemyScript);
        }
    }

    public static Enemy GetCreature(this Collider c)
    {
        return enemyScriptLookupFromCollider[c];
    }

    public static Player GetPlayer(this Transform t)
    {
        return playerScriptLookupFromTransform[t];
    }

}
