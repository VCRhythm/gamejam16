using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(EnemyActions))]
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : Creature {

    public string primaryTargetTag = "HomeBase";

    public float lookAheadStartDistance = 1f;
    public float lookAheadDistance = 5f;

    //Tower Effects
    public float towerSlowModifier = 1f;
    public float slowTimer;

    public int foodSpawnNumOnDeath;
    public int healthSpawnNumOnDeath;

    LayerMask lookLayer = 1 << 8 | 1 << 9 | 1 << 11;

    Transform model;
    EnemyMovement enemyMovement;
    EnemyActions enemyActions;
    Transform target;

    ItemSpawner itemSpawner;

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

    void Start()
    {
        itemSpawner = FindObjectOfType<ItemSpawner>();
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
        if (towerSlowModifier != 1f)
        {
            if (slowTimer <= Time.time)
            {
                towerSlowModifier = 1f;
            }
        }

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
                else if(obstacleTransform.CompareTag("Player"))
                {
                    FocusOnTag("HomeBase");
                }
            }

            enemyMovement.Move(heading, towerSlowModifier);
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
        return model.GetTransformInDirectionWithThreeRaycasts(model.forward * lookAheadStartDistance, heading, lookAheadDistance, lookLayer);
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

    protected override void Die()
    {
        for(int i=0; i < foodSpawnNumOnDeath; i++)
        {
            itemSpawner.Spawn(ItemSpawner.ItemType.Food, transform.position);
        }
        for(int i=0; i < healthSpawnNumOnDeath; i++)
        {
            itemSpawner.Spawn(ItemSpawner.ItemType.Health, transform.position);
        }

        base.Die();
    }

}