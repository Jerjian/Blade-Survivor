using UnityEngine;

public class LookTowardsMouse : MonoBehaviour
{
    private Player player;
    private Vector3 targetPosition;


    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {

        // Rotate the player towards the mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            direction.y = 0; // Keep the player upright
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = rotation;
        }
    }
}
