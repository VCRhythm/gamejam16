using UnityEngine;

public class CreatureMovement : MonoBehaviour {

    public float baseMoveModifier = .5f;
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

    protected bool Move(Vector3 direction, float moveModifier, out Transform obstacleTransform)
    {
        SmoothLook(direction);

        if (CanMoveInDirection(direction, out obstacleTransform))
        {
            rbody.MovePosition(rbody.position + direction * moveModifier);
            return true;
        }
        return false;
    }

    bool CanMoveInDirection(Vector3 direction, out Transform obstacleTransform)
    {
        obstacleTransform = GetTransformInDirection(direction);

        if (obstacleTransform != null)
        {
            return false;
        }
        else
        {
            obstacleTransform = null;
            return true;
        }
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

    Transform GetTransformInDirection(Vector3 direction)
    {
        RaycastHit[] hits = new RaycastHit[3];

        if(Physics.Raycast(rbody.position + model.forward * checkForCollisionStartDistance, direction, out hits[0], baseMoveModifier + checkForCollisionDistance, movementCheckLayer) ||
            Physics.Raycast(rbody.position + model.forward * checkForCollisionStartDistance, direction, out hits[1], baseMoveModifier + checkForCollisionDistance, movementCheckLayer) ||
            Physics.Raycast(rbody.position + model.forward * checkForCollisionStartDistance, direction, out hits[2], baseMoveModifier + checkForCollisionDistance, movementCheckLayer))
        {
            return hits[0].transform ?? hits[1].transform ?? hits[2].transform;
        }

        return null;
    }
}
