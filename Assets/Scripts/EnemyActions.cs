using UnityEngine;

public class EnemyActions : CreatureActions
{
    protected override void Awake()
    {
        base.Awake();

        attackLayer = structureLayer | playerLayer;
    }

    protected override bool CanAttack()
    {
        return base.CanAttack() && GetCreatureCollidersInAttackRadius().Length > 0;
    }

}
