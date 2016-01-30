using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : CreatureMovement {

    PlayerInput input;

    protected override void Awake()
    {
        base.Awake();

        input = GetComponent<PlayerInput>();
    }
	
	void FixedUpdate ()
    {
        Vector3 movement = GetInput();

        if (movement != Vector3.zero)
        {
            Transform obstacleTransform;
            Move(movement, out obstacleTransform);
        }
	}

    Vector3 GetInput()
    {
        return new Vector3(input.horizontalInput, 0, input.verticalInput);
    }
}
