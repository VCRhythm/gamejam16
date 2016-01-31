using UnityEngine;

public class CreatureMovement : MonoBehaviour {

    public float baseMoveModifier = .5f;
    public float turnModifier = 10f;
    public float checkForCollisionStartDistance = 1f;
    public float checkForCollisionDistance = 1f;

    const int enemyLayer = 1 << 8;
    const int structureLayer = 1 << 9;
    const int playerLayer = 1 << 11;
    protected LayerMask movementCheckLayer = enemyLayer | structureLayer | playerLayer;

    Rigidbody rbody;
    protected Transform model;
    protected Animator animator;

    protected virtual void Awake()
    {
        SetUpComponents();
    }

    public virtual bool Move(Vector3 direction, float moveModifier)
    {
        SmoothLook(direction);

        if (CanMoveInDirection(direction))
        {
            rbody.MovePosition(rbody.position + direction * baseMoveModifier * moveModifier);
            return true;
        }
        return false;
    }

    protected virtual bool CanMoveInDirection(Vector3 direction)
    {
        Transform obstacle = model.GetTransformInDirectionWithThreeRaycasts(model.forward * checkForCollisionStartDistance, direction, checkForCollisionDistance, movementCheckLayer);
        return !obstacle;
    }

    void SetUpComponents()
    {
        animator = GetComponentInChildren<Animator>();
        rbody = GetComponent<Rigidbody>();
        model = transform.GetChild(0);
    }

    void SmoothLook(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        model.rotation = Quaternion.Lerp(model.rotation, lookRotation, turnModifier * Time.deltaTime);
    }
}
