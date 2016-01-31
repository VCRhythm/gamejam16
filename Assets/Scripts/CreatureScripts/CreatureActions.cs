using UnityEngine;

public class CreatureActions : MonoBehaviour {

    //Attack Stats
    public float attackDistance = 3f;
    public float attackSphereRadius = 0.5f;
    public int attackDamage = 1;
    public float attackCooldown = 1f;

    protected const int enemyLayer = 1 << 8;
    protected const int structureLayer = 1 << 9;
    protected const int playerLayer = 1 << 11;
    protected LayerMask attackLayer;

    Transform model;
    protected float lastAttack;

    protected virtual void Awake ()
    {
        model = transform.FindChild("Model");
    }

    public void Attack()
    {
        if (!CanAttack()) return;

        lastAttack = Time.time;
        Collider[] attackedColliders = GetCreatureCollidersInAttackRadius();
        if(attackedColliders.Length > 0)
        {
            Damage(attackedColliders);
        }
    }

    void Damage(Collider[] attackedColliders)
    {
        for (int i = attackedColliders.Length - 1; i >= 0; i--)
        {
            Debug.Log(attackedColliders[i]);
            attackedColliders[i].GetIDamageable().TakeDamage(attackDamage);
        }
    }

    #region Helper Functions

    protected Collider[] GetCreatureCollidersInAttackRadius()
    {
        Debug.DrawRay(model.position + model.forward * attackDistance, model.forward * attackSphereRadius, Color.red, 1f);
        return Physics.OverlapSphere(model.position + model.forward * attackDistance, attackSphereRadius, attackLayer);
    }

    #endregion

    protected virtual bool CanAttack()
    {
        return Time.time - lastAttack > attackCooldown;
    }
}
