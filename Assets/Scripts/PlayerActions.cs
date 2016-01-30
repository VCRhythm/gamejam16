using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerActions : CreatureActions {

    PlayerInput input;

    protected override void Awake()
    {
        base.Awake();

        input = GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
        if(input.isAttacking && Time.time - lastAttack > attackAnimationLength)
        {
            Attack();
        }
    }

}
