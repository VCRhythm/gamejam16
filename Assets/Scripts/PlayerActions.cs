using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerActions : MonoBehaviour {

    public float attackDistance = 3f;
    public float attackSphereRadius = 0.5f;
    public int attackDamage = 1;
    public float attackAnimationLength = 1f;

    public LayerMask enemyLayer;

    private float lastAttack;

    PlayerInput input;
    Transform model;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        model = transform.FindChild("Model");
    }

    void FixedUpdate()
    {
        if(input.isAttacking && Time.time - lastAttack > attackAnimationLength)
        {
            Attack();
        }
    }

    void Attack()
    {
        lastAttack = Time.time;
        Collider[] attackedEnemyColliders = GetEnemyCollidersInAttackRadius();
        Damage(attackedEnemyColliders);
    }

    Collider[] GetEnemyCollidersInAttackRadius()
    {
        return Physics.OverlapSphere(model.position + model.forward, attackSphereRadius, enemyLayer);
    }

    void Damage(Collider[] attackedEnemyColliders)
    {
        for(int i = attackedEnemyColliders.Length - 1; i>=0; i--)
        {
            attackedEnemyColliders[i].GetEnemy().TakeDamage(attackDamage);
        }
    }
}
