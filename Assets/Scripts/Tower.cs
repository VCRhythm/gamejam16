using UnityEngine;
using System;

public abstract class Tower : MonoBehaviour, IDamageable {

    public float damage;
    public int health;
    public int range;
    public float fireRate;

    protected LayerMask enemyLayer = 1 << 8;
    protected GameObject target;

    //upgrading
    public int maxLevel;
    public int level = 1;

    //amount to increase stats each upgrade
    protected float upgradeDamage;
    protected int upgradeHealth;

    //Damage display
    public Material damagedMaterial;
    Material originalMaterial;
    MeshRenderer meshRenderer;
    bool isShowingDamage = false;

    void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        originalMaterial = meshRenderer.material;
    }

    void Start()
    {
        transform.Register();
    }

    virtual public void Upgrade()
    {

    }

    virtual protected void Fire()
    {

    }

    protected abstract GameObject GetTarget();

    #region IDamageable Interface

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        ShowDamage();
    }
    
    #endregion

    protected void ShowDamage()
    {
        if (isShowingDamage) return;

        isShowingDamage = true;
        meshRenderer.material = damagedMaterial;

        Invoke("HideDamage", 1f);
    }

    void HideDamage()
    {
        meshRenderer.material = originalMaterial;
        isShowingDamage = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
