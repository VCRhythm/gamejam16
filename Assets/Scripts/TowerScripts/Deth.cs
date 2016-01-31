using UnityEngine;

public class Deth : Tower {

    private float nextFire;

    // Use this for initialization
    void Start()
    {
        //generic stats to be changed
        health = 300;
        damage = 20;
        cost = 300;
        range = 15;
        fireRate = .5f;
        upgradeDamage = 3;
        upgradeHealth = 10;
        maxLevel = 5;
        target = null;
        nextFire = 0;
    }

    // Update is called once per frame
    void Update()
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
            target.transform.GetCreature().TakeDamage((int)damage);
        }
    }
}
