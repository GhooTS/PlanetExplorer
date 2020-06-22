using UnityEngine;

public class FlipableObject : FlipableObjectBase
{
    public override bool FlipX
    {
        get { return transform.localRotation.eulerAngles.y == 180; }
        set { FlipAroundYAxis(value); }
    }
    public override bool FlipY
    {
        get { return transform.localRotation.eulerAngles.x == 180; }
        set { FlipAroundXAxis(value); }
    }


    private void FlipAroundYAxis(bool flip)
    {
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.y = flip ? 180 : 0;
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    private void FlipAroundXAxis(bool flip)
    {
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.x = flip ? 180 : 0;
        transform.localRotation = Quaternion.Euler(currentRotation);
    }
}
