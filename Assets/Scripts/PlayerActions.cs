using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerActions : MonoBehaviour {

    public float attackSphereDiameter = 1;
    public Vector3 attackDirection = new Vector3(0, 0, 2f);
    public int attackDamage = 1;

    PlayerInput input;
    LayerMask enemyLayer = 8;

    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
        if(input.isAttacking)
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider[] attackedEnemyColliders = GetEnemyCollidersInAttackRadius();
        Damage(attackedEnemyColliders);
    }

    Collider[] GetEnemyCollidersInAttackRadius()
    {
        return Physics.OverlapSphere(attackDirection, attackSphereDiameter, enemyLayer);
    }

    void Damage(Collider[] attackedEnemyColliders)
    {
        for(int i=attackedEnemyColliders.Length - 1; i>=0; i--)
        {
            attackedEnemyColliders[i].GetEnemy().TakeDamage(attackDamage);
        }
    }
}
