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
            Transform obstacleTransform;
            Move(movement, baseMoveModifier * (input.isRunning ? runModifier : 1), out obstacleTransform);
        }
	}

    Vector3 GetMovementInput()
    {
        return new Vector3(input.horizontalInput, 0, input.verticalInput);
    }
}
