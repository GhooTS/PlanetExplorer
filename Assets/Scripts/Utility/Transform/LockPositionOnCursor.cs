using UnityEngine;

public class LockPositionOnCursor : MonoBehaviour
{
    public Camera cam;
    public float z;

    private void Update()
    {
        Vector3 newPosition = cam.GetMouseWorldPosition();
        newPosition.z = z;
        transform.position = newPosition;
    }
}
