using UnityEngine;
using System.Collections;

public class GunTower : Tower {

    private float nextFire;

	// Use this for initialization
	void Start ()
    {
        //generic stats to be changed
        health = 10;
        damage = 1;
        range = 5;
        fireRate = 1f;
        upgradeDamage = 3;
        upgradeHealth = 10;
        maxLevel = 5;
        target = null;
        nextFire = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //check if tower has target
	    if (!target)
        {
            //get a new target
            target = GetTarget();
        }
        else
        {
            //check if target is out of range
            if ((target.transform.position - transform.position).magnitude > range)
            {
                //remove out of range target
                target = null;
            }
            else
            {
                //fire at target
                if (nextFire < Time.time)
                {
                    Fire();
                    nextFire = Time.time + fireRate;
                }
            }
        }
	}

    override protected GameObject GetTarget()
    {
        GameObject closestTarget;
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, range, enemyLayer);
        if (enemiesInRange.Length > 0)
        {
            //get closest enemy
            closestTarget = enemiesInRange[0].gameObject;
            for (int i = 0; i < enemiesInRange.Length; i++)
            {
                GameObject nextTarget = enemiesInRange[i].gameObject;
                if ((nextTarget.transform.position - transform.position).magnitude < (closestTarget.transform.position - transform.position).magnitude)
                {
                    closestTarget = nextTarget;
                }
            }
            return closestTarget;
        }
        return null;
    }

    //add to stats if upgraded
    override public void Upgrade()
    {
        level++;
        health += upgradeHealth;
        damage += upgradeDamage;
    }

    protected override void Fire()
    {
        if (target)
        {
            target.GetComponent<Creature>().TakeDamage((int)damage);
        }
    }
}
