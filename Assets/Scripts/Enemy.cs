public class Enemy : Creature {
    EnemyMovement enemyMovement;

    protected override void Awake()
    {
        base.Awake();

        color = AssignColor();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if(health > 0)
        {
            enemyMovement.FocusOnTag("Player");
        }
    }
}