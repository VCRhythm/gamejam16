using UnityEngine;
using System.Collections;

public class WallTower : Tower
{

    override public void Upgrade()
    {
        level++;
        health += upgradeHealth;
    }


}