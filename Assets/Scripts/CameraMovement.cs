using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;
    public float dampTime = 1f;

    Vector3 currentVelocity;

    void Start()
    {
        transform.position = target.position + targetOffset;
        transform.LookAt(target.position);
    }

    void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (target)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position + targetOffset, ref currentVelocity, dampTime * Time.deltaTime);
        }
    }
}
