using UnityEngine;

public class CreatureMovement : MonoBehaviour {

    public float baseMoveModifier = .5f;
    public float towerSlowModifier = 1f;
    public float slowTimer;
    public float turnModifier = 10f;
    public float checkForCollisionStartDistance = 1f;
    public float checkForCollisionDistance = 1f;

    const int enemyLayer = 1 << 8;
    const int structureLayer = 1 << 9;
    const int playerLayer = 1 << 11;
    LayerMask movementCheckLayer = enemyLayer | structureLayer | playerLayer;

    Rigidbody rbody;
    Transform model;

    protected virtual void Awake()
    {
        SetUpComponents();
    }

    public virtual bool Move(Vector3 direction, float moveModifier)
    {
        SmoothLook(direction);

        if (CanMoveInDirection(direction))
        {
            rbody.MovePosition(rbody.position + direction * baseMoveModifier * moveModifier * towerSlowModifier);
            return true;
        }
        return false;
    }

    bool CanMoveInDirection(Vector3 direction)
    {
        Transform obstacle = model.GetTransformInDirection(model.forward * checkForCollisionStartDistance, direction, checkForCollisionDistance, movementCheckLayer);
        return !obstacle;
    }

    void SetUpComponents()
    {
        rbody = GetComponent<Rigidbody>();
        model = transform.FindChild("Model");
    }

    void SmoothLook(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        model.rotation = Quaternion.Lerp(model.rotation, lookRotation, turnModifier * Time.deltaTime);
    }
}
