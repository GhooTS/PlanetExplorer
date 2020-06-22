using UnityEngine;


[System.Serializable]
public class MovePositionCalculator
{
    public SurfaceSnap surfaceSnap;
    public GridSnap gridSnap;

    Vector2 lastMousePosition;


    public Vector3 Move(Transform transform,MoveData moveable, Vector2 currentMousePosition, Vector2 offset)
    {
        Vector3 output = transform.position;
        Vector2 mouseDelta;
        switch (moveable.moveMode)
        {
            case MoveData.MoveMode.free:
                output = currentMousePosition + offset;
                break;
            case MoveData.MoveMode.snapToGrid:
                output = gridSnap.Snap(currentMousePosition + offset);
                break;
            case MoveData.MoveMode.snapToGround:
                mouseDelta = lastMousePosition - currentMousePosition;
                output = surfaceSnap.Snap(transform, currentMousePosition, mouseDelta, moveable.snapDirection, moveable.groundLayer,moveable.upSideDown);
                lastMousePosition = currentMousePosition;
                break;
            case MoveData.MoveMode.snapToGroundAndGrid:
                mouseDelta = lastMousePosition - currentMousePosition;
                output = surfaceSnap.Snap(transform, currentMousePosition, mouseDelta, moveable.snapDirection, moveable.groundLayer, moveable.upSideDown);
                lastMousePosition = currentMousePosition;
                output = gridSnap.Snap(output);
                break;

        }

        return output;
    }


}
