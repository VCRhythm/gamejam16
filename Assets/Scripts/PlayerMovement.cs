using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour {

    public float moveModifier = .5f;

    PlayerInput input;
    Rigidbody rigidbody;
    Transform model;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
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
        model.LookAt(transform.position + direction);
        rigidbody.MovePosition(rigidbody.position + direction * moveModifier);
        
        //rigidbody..Translate(direction * moveModifier);
    }
}
