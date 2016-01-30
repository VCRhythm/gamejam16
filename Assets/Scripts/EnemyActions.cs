public class EnemyActions : CreatureActions
{

    protected override void Awake()
    {
        base.Awake();

        attackLayer = structureLayer | playerLayer;
    }
}
