using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : CreatureMovement {

    public float runModifier = 2f;
    PlayerInput input;

    protected override void Awake()
    {
        base.Awake();

        input = GetComponent<PlayerInput>();
    }
	
	void FixedUpdate ()
    {
        Vector3 movement = GetMovementInput();

        if (movement != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            Move(movement, input.isRunning ? runModifier : 1);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    protected override bool CanMoveInDirection(Vector3 direction)
    {
        Transform obstacle = model.GetTransformInDirection(model.forward * checkForCollisionStartDistance, direction, checkForCollisionDistance, movementCheckLayer);
        return !obstacle;
    }

    Vector3 GetMovementInput()
    {
        return new Vector3(input.horizontalInput, 0, input.verticalInput);
    }
}
