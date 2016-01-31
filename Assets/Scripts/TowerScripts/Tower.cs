using UnityEngine;

public abstract class Tower : MonoBehaviour, IDamageable {

    public float damage;
    public int health;
    public int range;
    public float fireRate;
    public int cost;

    protected LayerMask enemyLayer = 1 << 8;
    protected GameObject target;
    new protected ParticleSystem particleSystem;

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
        particleSystem = GetComponentInChildren<ParticleSystem>();
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
        if (particleSystem)
        {
            particleSystem.Play();
        }
    }

    protected GameObject GetTarget()
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
