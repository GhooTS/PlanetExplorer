using UnityEngine;

public static class TranslateExtension
{
    public static void AssignTransformFromMatix(this Transform transform, Matrix4x4 matrix)
    {
        transform.position = new Vector3(matrix.m03, matrix.m13, matrix.m23);
        transform.rotation = matrix.rotation;
        transform.localScale = matrix.lossyScale;
    }
}
