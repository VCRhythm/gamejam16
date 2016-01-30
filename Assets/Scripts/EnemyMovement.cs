using UnityEngine;
using System.Collections.Generic;

public class EnemyMovement : CreatureMovement {

    public string primaryTargetTag = "HomeBase";
    public EnemyTarget targetPrefab;

    private enum AvoidTurn
    {
        Left = -1,
        Right = 1
    }

    Transform target;
    float turnAngle = 75;
    AvoidTurn preferredTurn;

    EnemyTarget creatureTarget;
    List<string> avoidTags = new List<string> { "Obstacle", "Enemy" };
    string playerTag = "Player";

    Enemy enemy;
    EnemyActions enemyActions;

    protected override void Awake()
    {
        base.Awake();

        enemy = GetComponent<Enemy>();
        enemyActions = GetComponent<EnemyActions>();
    }

    void Start()
    {
        preferredTurn = RandomPreferredTurn();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 heading = (target.position - transform.position).normalized;
            Transform obstacleTransform;

            if (!Move(heading, out obstacleTransform))
            {
                if(ShouldAvoid(obstacleTransform))
                {
                    target = CreateAvoidanceTarget(heading);
                }
                else if(ShouldAttack(obstacleTransform))
                {
                    enemyActions.Attack();

                    if (enemy.stayOnPlayer)
                    {
                        primaryTargetTag = obstacleTransform.tag;
                        target = SetPrimaryTarget();
                    }
                }
            }
        }
        else
        {
            target = SetPrimaryTarget();
        }
    }

    Transform CreateAvoidanceTarget(Vector3 heading)
    {
        if (creatureTarget) return creatureTarget.transform;

        creatureTarget =  Instantiate(targetPrefab, transform.position + Quaternion.Euler(0, (int)preferredTurn * turnAngle, 0) * heading * 5, Quaternion.identity) as EnemyTarget;
        creatureTarget.targetEnemy = creature as Enemy;

        return creatureTarget.transform;
    } 

    Transform SetPrimaryTarget()
    {
        return GameObject.FindWithTag(primaryTargetTag).transform;

    }

    bool ShouldAvoid(Transform obstacleTransform)
    {
        return avoidTags.Contains(obstacleTransform.tag);
    }

    bool ShouldAttack(Transform obstacleTransform)
    {
        return obstacleTransform.CompareTag(playerTag);
    }

    AvoidTurn RandomPreferredTurn()
    {
        return Random.Range(0,2) == 0 ? AvoidTurn.Left : AvoidTurn.Right;
    }
}
