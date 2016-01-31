using UnityEngine;

public class Fan : Tower {

    private float nextFire;
    public float slowRate = 1f;

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
                    if (target.transform.GetEnemy().towerSlowModifier == 1f)
                    {
                        Fire();
                        nextFire = Time.time + fireRate;
                    }
                }
            }
        }
    }

    override public void Upgrade()
    {
        //increase slow time
        if (level < maxLevel)
        {
            level++;
            damage -= upgradeDamage;
            health += upgradeHealth;
        }
    }

    protected override void Fire()
    {
        if (target)
        {
            Enemy enemy = target.transform.GetEnemy();
            enemy.towerSlowModifier = .25f;
            enemy.slowTimer = Time.time + slowRate;
        }
    }
}
