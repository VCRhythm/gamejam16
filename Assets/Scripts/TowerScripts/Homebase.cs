using UnityEngine;

public class Homebase : Tower
{
    public int heal = 2;
    public float healRate = 5f;
    public int maxHealth;

    float nextHealTime = 0;

    void Update()
    {
        if (health < maxHealth && nextHealTime < Time.time)
        {
            Heal();
        }
    }

    void Heal()
    {
        health += 10;
        
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        nextHealTime = Time.time + healRate;
    }

    protected override void Die()
    {
        enabled = false;
    }

}
