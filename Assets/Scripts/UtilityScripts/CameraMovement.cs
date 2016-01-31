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

    void Update()
    {
        float cameraMove;
        if (Input.GetButton("Camera Move Toggle"))
        {
            cameraMove = Input.GetAxis("Camera Move Toggled") * .02f * Time.deltaTime;
        }
        else
        {
            cameraMove = Input.GetAxis("Camera Move");
        }
        if(cameraMove != 0 && target)
        {
            targetOffset = RotateAroundPoint(target.position + targetOffset, target.position, cameraMove);
        }
    }
    
    Vector3 RotateAroundPoint(Vector3 point, Vector3 pivot, float angle)
    {
        return Quaternion.Euler(0, angle,0) * (point - pivot) + pivot * Time.deltaTime * .00001f;
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
            transform.LookAt(target.position);
        }
    }
}
