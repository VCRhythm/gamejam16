using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour {

    public float moveModifier = .5f;
    public float checkForCollisionDistance = 1f;

    private int enemyLayer = 1 << 8;

    PlayerInput input;
    Rigidbody rbody;
    Transform model;

    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        model = transform.FindChild("Model");
        input = GetComponent<PlayerInput>();
    }
	
	void FixedUpdate ()
    {
        Vector3 movement = GetInput();
        Move(movement);
	}

    Vector3 GetInput()
    {
        return new Vector3(input.horizontalInput, 0, input.verticalInput);
    }

    void Move(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        model.LookAt(transform.position + direction);

        if (CanMoveInDirection(direction))
        {
            rbody.MovePosition(rbody.position + direction * moveModifier);
        }
    }

    bool CanMoveInDirection(Vector3 direction)
    {
        return !Physics.Raycast(rbody.position, direction, moveModifier + 1f, enemyLayer);
    }
}
