using UnityEngine;

public class CreatureMovement : MonoBehaviour {

    public float moveModifier = .5f;
    public float turnModifier = 10f;
    public float checkForCollisionStartDistance = 1f;
    public float checkForCollisionDistance = 1f;

    protected const int enemyLayer = 1 << 8;
    const int structureLayer = 1 << 9;
    const int playerLayer = 1 << 11;
    protected LayerMask movementCheckLayer = enemyLayer | structureLayer | playerLayer;

    Rigidbody rbody;
    Transform model;
    protected Creature creature;

    protected virtual void Awake()
    {
        SetUpComponents();
    }

    protected bool Move(Vector3 direction, out Transform obstacleTransform)
    {
        //model.LookAt(transform.position + direction);
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
        RaycastHit hit;
        //return !Physics.SphereCast(rbody.position + model.forward * checkForCollisionStartDistance, moveModifier + checkForCollisionDistance, direction, out hit, movementCheckLayer);
        if(Physics.Raycast(rbody.position + model.forward * checkForCollisionStartDistance, direction, out hit, moveModifier + checkForCollisionDistance, movementCheckLayer))
        {
            obstacleTransform = hit.transform;
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
        creature = GetComponent<Creature>();
        rbody = GetComponent<Rigidbody>();
        model = transform.FindChild("Model");
    }

    void SmoothLook(Vector3 direction)
    {
        model.rotation = Quaternion.Lerp(model.rotation, Quaternion.LookRotation(direction), turnModifier * Time.deltaTime);
    }
}
