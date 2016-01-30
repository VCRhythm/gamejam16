using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerActions : CreatureActions {

    PlayerInput input;
    Player player;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<Player>();
        input = GetComponent<PlayerInput>();
        attackLayer = enemyLayer;
    }

    void FixedUpdate()
    {
        if(input.isAttacking)
        {
            Attack();
        }

        if (input.colorCycle != 0)
        {
            player.CycleColor(input.colorCycle > 0);
        }
    }

}
