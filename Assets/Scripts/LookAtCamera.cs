using UnityEngine;

public class LookAtCamera : MonoBehaviour
{


    private void LateUpdate()
    {
        Vector3 direction = (Camera.main.transform.position - transform.position).normalized;
        // Calculate the angle between the health bar's forward direction and the direction to the camera
        float angle = Vector3.Angle(transform.forward, direction);

        if (angle > 180)
        {
            direction = -direction;
        }
        direction = new Vector3(direction.x, 0, direction.z);
        transform.LookAt(Camera.main.transform, direction);
    }
}