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
    List<string> attackTags = new List<string> { "Player", "HomeBase", "Tower" };

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
            if (!Move(heading, baseMoveModifier, out obstacleTransform))
            {
                if(ShouldAvoid(obstacleTransform))
                {
                    target = CreateAvoidanceTarget(heading);
                }
                else if(ShouldAttack(obstacleTransform))
                {
                    enemyActions.Attack();

                    if (ShouldFollow(obstacleTransform))
                    {
                        FocusOnTag(obstacleTransform.tag);
                    }
                }
            }
        }
        else
        {
            target = SetPrimaryTarget();
        }
    }

    public void FocusOnTag(string tag)
    {
        primaryTargetTag = tag;
        target = SetPrimaryTarget();
    }

    #region Private Functions

    Transform CreateAvoidanceTarget(Vector3 heading)
    {
        if (creatureTarget)
        {
            creatureTarget.transform.position = GetRotatedHeading(heading);
        }
        else
        {
            creatureTarget = Instantiate(targetPrefab, GetRotatedHeading(heading), Quaternion.identity) as EnemyTarget;
            creatureTarget.targetEnemy = enemy;
        }

        return creatureTarget.transform;
    } 

    Transform SetPrimaryTarget()
    {
        GameObject target = GameObject.FindWithTag(primaryTargetTag);
        return target ? target.transform : null;
    }

    bool ShouldAvoid(Transform obstacleTransform)
    {
        return avoidTags.Contains(obstacleTransform.tag);
    }

    bool ShouldAttack(Transform obstacleTransform)
    {
        return attackTags.Contains(obstacleTransform.tag);
    }

    bool ShouldFollow(Transform obstacleTransform)
    {
        return obstacleTransform.CompareTag("Player") && obstacleTransform.GetCreature().color == (enemy as Creature).color;
    }

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