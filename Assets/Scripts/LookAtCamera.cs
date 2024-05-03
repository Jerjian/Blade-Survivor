using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}