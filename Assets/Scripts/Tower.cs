using UnityEngine;
using System.Collections;

public abstract class Tower : MonoBehaviour {

    public int damage;
    public int health;
    public int range;
    public float fireRate;

    protected LayerMask enemyLayer = 1 << 8;
    protected GameObject target;

    //upgrading
    public int maxLevel;
    public int level = 1;

    //amount to increase stats each upgrade
    protected int upgradeDamage;
    protected int upgradeHealth;

    virtual public void Upgrade()
    {

    }

    virtual protected void Fire()
    {

    }

    protected abstract GameObject GetTarget();
}
