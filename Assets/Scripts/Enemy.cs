using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(EnemyActions))]
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : Creature {

    public string primaryTargetTag = "HomeBase";


    public float lookAheadStartDistance = 1f;
    public float lookAheadDistance = 5f;
    LayerMask lookLayer = 1 << 8 | 1 << 9 | 1 << 11;

    Transform model;
    EnemyMovement enemyMovement;
    EnemyActions enemyActions;
    Transform target;

    List<string> avoidTags = new List<string> { "Obstacle", "Enemy" };
    List<string> attackTags = new List<string> { "Player", "HomeBase", "Tower" };

    protected override void Awake()
    {
        base.Awake();

        model = transform.FindChild("Model");
        color = AssignColor();
        enemyActions = GetComponent<EnemyActions>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if(health > 0)
        {
            FocusOnTag("Player");
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 heading = (target.position - transform.position).normalized;
            Transform obstacleTransform = LookAheadForObjects(heading);

            if (obstacleTransform)
            {
                if (ShouldAvoid(obstacleTransform))
                {
                    target = enemyMovement.CreateAvoidanceTarget(heading);
                }


                if (ShouldAttack(obstacleTransform))
                {
                    enemyActions.Attack();
                }

                if (ShouldFollow(obstacleTransform))
                {
                    FocusOnTag(obstacleTransform.tag);
                }
            }

            enemyMovement.Move(heading, 1);
        }
        else
        {
            target = SetPrimaryTarget();
        }
    }

    bool ShouldAttack(Transform obstacleTransform)
    {
        return attackTags.Contains(obstacleTransform.tag);
    }

    bool ShouldAvoid(Transform obstacleTransform)
    {
        return avoidTags.Contains(obstacleTransform.tag);
    }

    bool ShouldFollow(Transform obstacleTransform)
    {
        return obstacleTransform.CompareTag("Player") && obstacleTransform.GetCreature().color == color;
    }

    Transform LookAheadForObjects(Vector3 heading)
    {
        return model.GetTransformInDirection(model.forward * lookAheadStartDistance, heading, lookAheadDistance, lookLayer);
    }

    void FocusOnTag(string tag)
    {
        primaryTargetTag = tag;
        target = SetPrimaryTarget();
    }

    Transform SetPrimaryTarget()
    {
        GameObject target = GameObject.FindWithTag(primaryTargetTag);
        return target ? target.transform : null;
    }

}