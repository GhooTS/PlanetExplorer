using UnityEngine;

[System.Serializable]
public class ArcFilter
{
    [Range(0, 360)]
    public float startArc = 0;
    [Range(0, 360)]
    public float arcSize = 360;
    public bool flipX = false;
    public bool flipY = false;
    public bool offsetByZRotation = true;
    [HideInInspector]
    public Transform offsetTransform;

    private Vector2 arcCenterVector = Vector3.zero;
    public Vector2 ArcCenterVector
    {
        get
        {
            UpdateArcCenterVector();
            return arcCenterVector;
        }
    }
    public float ArcHalfSize { get { return arcSize / 2; } }
    public float ArcStartAngle { get { return startArc + GetArcOffset(); } }
    public float CurrentArcStartAngle { get { return currentArrStart; } }
    public float ArcEndAngle { get { return arcSize + GetArcOffset(); } }

    //private float centerAngle;
    private float currentArrStart;


    public bool Pass(Vector2 from, Vector2 target)
    {
        Vector2 direction = target - from;
        float angle = Vector2.Angle(ArcCenterVector, direction.normalized);

        return Pass(angle);
    }

    public bool Pass(float angle)
    {
        return angle <= ArcHalfSize;
    }

    private float GetArcCenterAngle()
    {
        return Mathf.Abs(startArc + arcSize / 2);
    }

    private float GetArcOffset()
    {
        float output = offsetByZRotation ? offsetTransform.rotation.eulerAngles.z : 0;
        output = flipX ? flipY ? output : -output
                       : flipY ? -output : output;
        return output;
    }

    public void UpdateArcCenterVector()
    {
        float centerAngle = GetArcCenterAngle() + GetArcOffset();
        arcCenterVector.x = (flipX ? -1 : 1) * Mathf.Cos(centerAngle * Mathf.Deg2Rad);
        arcCenterVector.y = (flipY ? -1 : 1) * Mathf.Sin(centerAngle * Mathf.Deg2Rad);
        if (flipX || flipY)
        {
            currentArrStart = Vector2.SignedAngle(Vector2.right, arcCenterVector) - arcSize / 2 - GetArcOffset();
        }
        else
        {
            currentArrStart = centerAngle - arcSize / 2;
        }
    }
}
