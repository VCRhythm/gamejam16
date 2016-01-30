public class Enemy : Creature {
    public bool stayOnPlayer = true;

    EnemyMovement enemyMovement;

    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if(health > 0 && stayOnPlayer)
        {
            enemyMovement.FocusOnTag("Player");
        }
    }
}