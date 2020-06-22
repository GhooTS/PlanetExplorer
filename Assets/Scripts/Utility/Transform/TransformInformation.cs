using UnityEngine;

public class TransformInformation
{
    Vector3 position;
    Quaternion rotation;
    Vector3 scale;

    public TransformInformation(Transform transform)
    {
        position = transform.position;
        rotation = transform.rotation;
        scale = transform.localScale;
    }


    public void Assign(Transform transform)
    {
        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale;
    }
}
