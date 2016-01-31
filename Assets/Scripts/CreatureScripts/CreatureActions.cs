using UnityEngine;
using System.Collections;

public class CreatureActions : MonoBehaviour {

    //Attack Stats
    public float attackDistance = 3f;
    public float attackSphereRadius = 0.5f;
    public int attackDamage = 1;
    public float attackCooldown = 1f;

    [HideInInspector] public bool isActing = false;

    protected const int enemyLayer = 1 << 8;
    protected const int structureLayer = 1 << 9;
    protected const int playerLayer = 1 << 11;
    protected LayerMask attackLayer;

    Transform model;
    protected float lastAttack;
    Animator animator;

    protected virtual void Awake ()
    {
        model = transform.GetChild(0);
        animator = GetComponentInChildren<Animator>();
    }

    public void Attack()
    {
        if (!CanAttack()) return;

        isActing = true;
        animator.SetTrigger("Attack");

        StartCoroutine(AttackCoroutine());        
    }

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(.5f);
        Collider[] attackedColliders = GetCreatureCollidersInAttackRadius();
        if (attackedColliders.Length > 0)
        {
            Damage(attackedColliders);
        }
        isActing = false;
    }

    void Damage(Collider[] attackedColliders)
    {
        for (int i = attackedColliders.Length - 1; i >= 0; i--)
        {
            attackedColliders[i].GetIDamageable().TakeDamage(attackDamage);
        }
    }

    #region Helper Functions

    protected Collider[] GetCreatureCollidersInAttackRadius()
    {
        return Physics.OverlapSphere(model.position + model.forward * attackDistance, attackSphereRadius, attackLayer);
    }

    #endregion

    protected virtual bool CanAttack()
    {
        return Time.time - lastAttack > attackCooldown;
    }
}
