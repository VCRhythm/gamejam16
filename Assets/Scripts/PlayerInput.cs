using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public float horizontalInput {

        get; private set;
    }

    public float verticalInput
    {
        get; private set;
    }

    public bool isAttacking
    {
        get; private set;
    }

	void Update ()
    {
        isAttacking = Input.GetButtonDown("Attack");
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
	}
}
