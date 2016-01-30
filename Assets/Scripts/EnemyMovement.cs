using UnityEngine;

public class EnemyMovement : CreatureMovement {

    public EnemyTarget targetPrefab;
    EnemyTarget enemyTarget;

    private enum AvoidTurn
    {
        Left = -1,
        Right = 1
    }

    float turnAngle = 75;
    AvoidTurn preferredTurn;

    Enemy enemy;

    protected override void Awake()
    {
        base.Awake();

        enemy = GetComponent<Enemy>();
    }

    void Start()
    {
        preferredTurn = RandomPreferredTurn();
    }

    public Transform CreateAvoidanceTarget(Vector3 heading)
    {
        if (enemyTarget)
        {
            enemyTarget.transform.position = GetRotatedHeading(heading);
        }
        else
        {
            enemyTarget = Instantiate(targetPrefab, GetRotatedHeading(heading), Quaternion.identity) as EnemyTarget;
            enemyTarget.targetEnemy = enemy;
        }

        return enemyTarget.transform;
    }

    #region Private Functions

    AvoidTurn RandomPreferredTurn()
    {
        return Random.Range(0,2) == 0 ? AvoidTurn.Left : AvoidTurn.Right;
    }

    Vector3 GetRotatedHeading(Vector3 heading)
    {
        return transform.position + Quaternion.Euler(0, (int)preferredTurn * turnAngle, 0) * heading * 5;
    }

    #endregion
}