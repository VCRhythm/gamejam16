using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : CreatureMovement {

    public float runModifier = 2f;
    PlayerInput input;
    AnimationState moveAnimation;

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
            Move(Camera.main.transform.TransformDirection(movement), input.isRunning ? runModifier : 1);
            animator.SetBool("IsMoving", true);
            animator.SetFloat("RunMod", input.isRunning ? 2 : 1);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    protected override bool CanMoveInDirection(Vector3 direction, float modifier)
    {
        Transform obstacle = model.GetTransformInDirection(model.forward * checkForCollisionStartDistance, direction, checkForCollisionDistance * modifier, movementCheckLayer);
        return !obstacle;
    }

    Vector3 GetMovementInput()
    {
        return new Vector3(input.horizontalInput, 0, input.verticalInput);
    }
}
