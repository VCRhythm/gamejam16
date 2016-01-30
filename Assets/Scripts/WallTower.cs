using UnityEngine;
using System.Collections;

public class WallTower : Tower {

	// Use this for initialization
	void Start () {
        health = 10;
        damage = 15;
        upgradeDamage = 3;
        upgradeHealth = 10;
        maxLevel = 5;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    override protected GameObject GetTarget()
    {
        return null;
    }

    override public void Upgrade()
    {
        level++;
        health += upgradeHealth;
        damage += upgradeDamage;
    }

    void OnColliderEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("enemy");
            health--;
            //deal damage to enemy and push it back
            other.transform.Translate((other.transform.position - transform.position) * 2);
        }
        if (health == 0)
        {
            Destroy(this);
        }
    }
}
