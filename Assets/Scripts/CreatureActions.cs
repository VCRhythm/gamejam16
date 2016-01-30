using UnityEngine;

public class CreatureActions : MonoBehaviour {

    public float attackDistance = 3f;
    public float attackSphereRadius = 0.5f;
    public int attackDamage = 1;
    public float attackAnimationLength = 1f;

    public LayerMask attackLayer;

    Transform model;
    protected float lastAttack;

    protected virtual void Awake ()
    {
        model = transform.FindChild("Model");
    }

    public void Attack()
    {
        lastAttack = Time.time;
        Collider[] attackedColliders = GetCreatureCollidersInAttackRadius();
        Damage(attackedColliders);
    }

    Collider[] GetCreatureCollidersInAttackRadius()
    {
        return Physics.OverlapSphere(model.position + model.forward, attackSphereRadius, attackLayer);
    }

    void Damage(Collider[] attackedCreatureColliders)
    {
        for (int i = attackedCreatureColliders.Length - 1; i >= 0; i--)
        {
            attackedCreatureColliders[i].GetCreature().TakeDamage(attackDamage);
        }
    }
}
