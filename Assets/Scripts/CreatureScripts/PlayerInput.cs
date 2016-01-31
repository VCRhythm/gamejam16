using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public float horizontalInput { get; private set; }
    public float verticalInput { get; private set; }
    public bool isAttacking { get; private set; }
    public bool isRunning { get; private set; }
    public bool isTogglingBuildMenu { get; private set; }
    public float colorCycle { get; private set; }

    void Update ()
    {
        colorCycle = Input.GetAxisRaw("Cycle Color");
        isTogglingBuildMenu = Input.GetButtonDown("Build Menu");
        isRunning = Input.GetButton("Run");
        isAttacking = Input.GetButtonDown("Attack");
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
	}
}
