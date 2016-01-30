using UnityEngine;
using System;

public class Homebase : Tower
{

    public int heal = 2;
    public float healRate = 5f;
    public int maxHealth;

    float nextHealTime = 0;

    void Start()
    {
        health = 700;
        maxHealth = health;
        target = null;
        range = 10;
        fireRate = 2.5f;
    }

    void Update()
    {
        if (health < maxHealth && nextHealTime < Time.time)
        {
            Heal();
        }
    }

    void Heal()
    {
        if (health <= (health - 10))
        {
            health += 10;
        }
        else if (health > (maxHealth - 10))
        {
            health = maxHealth;
        }
        nextHealTime = Time.time + healRate;
    }



    protected override GameObject GetTarget()
    {
        throw new NotImplementedException();
    }
}
