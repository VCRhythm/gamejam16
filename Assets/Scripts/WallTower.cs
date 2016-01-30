using UnityEngine;
using System.Collections;

public class WallTower : Tower
{

    // Use this for initialization
    void Start()
    {
        health = 10;
        upgradeHealth = 10;
        maxLevel = 5;
    }


    override protected GameObject GetTarget()
    {
        return null;
    }

    override public void Upgrade()
    {
        level++;
        health += upgradeHealth;
    }


}