using UnityEngine;

[RequireComponent(typeof(ObjectDetector))]
public class ObjectDetectorFlipable : FlipableObjectBase
{
    private ObjectDetector objectDetector;
    public override bool FlipX
    {
        get { return objectDetector.ArcFlipX; }
        set { objectDetector.ArcFlipX = value; }
    }
    public override bool FlipY
    {
        get { return objectDetector.ArcFlipY; }
        set { objectDetector.ArcFlipY = value; }
    }

    private void Awake()
    {
        objectDetector = GetComponent<ObjectDetector>();
    }
}
