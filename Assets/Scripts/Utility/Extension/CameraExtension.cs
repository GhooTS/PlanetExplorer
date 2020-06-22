using UnityEngine;



public static class CameraExtension
{
    public static Vector3 GetMouseWorldPosition(this Camera camera)
    {
        return camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
