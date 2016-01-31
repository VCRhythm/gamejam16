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
            Move(movement, input.isRunning ? runModifier : 1);
        }
	}

    Vector3 GetMovementInput()
    {
        return new Vector3(input.horizontalInput, 0, input.verticalInput);
    }
}
