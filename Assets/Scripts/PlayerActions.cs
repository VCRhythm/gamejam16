using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerActions : CreatureActions {

    PlayerInput input;

    protected override void Awake()
    {
        base.Awake();

        input = GetComponent<PlayerInput>();
        attackLayer = enemyLayer;
    }

    void FixedUpdate()
    {
        if(input.isAttacking && Time.time - lastAttack > attackAnimationLength)
        {
            Attack();
        }
    }

}
