using System;
using UnityEngine;



[CreateAssetMenu(menuName = "Ingame tools/Snaps/Ground Snap")]
public class SurfaceSnap : ScriptableObject
{
    [Flags]
    public enum SnapDirection
    {
        left = 1,
        right = 2,
        up = 4,
        down = 8
    }

    public float snapDistance = 1f;
    public float maxSlope = 45f;
    public Vector2 SlideDirection { get; private set; }
    public bool Grounded { get; private set; }
    public Vector2 GroundPosition { get; private set; }
    private Vector2 normal;


    private void ChceckForSurface(Transform transform, Vector2 mousePosition, SnapDirection snapDirection, LayerMask lasyerMask,bool upSideDown = false)
    {
        Grounded = false;
        Vector2 surfaceDirection = GetSnapDirecton(snapDirection);


        RaycastHit2D hit = Physics2D.Raycast(mousePosition - surfaceDirection, surfaceDirection, snapDistance + 1, lasyerMask);
        Debug.DrawRay(mousePosition - surfaceDirection, surfaceDirection,Color.green,0.1f);
        if (hit.collider != null)
        {
            normal = hit.normal;
            if (Mathf.Abs(Vector2.Angle(-surfaceDirection, normal)) > maxSlope)
            {
                transform.rotation = Quaternion.identity;
                return;
            }
            GroundPosition = hit.point;

            float x = -hit.normal.y;
            float y = hit.normal.x;

            x = surfaceDirection.y < 0 ? x : -x;
            y = surfaceDirection.x < 0 ? -y : y;
            SlideDirection = new Vector2(x, y);

            float zRotation = Vector2.SignedAngle(-surfaceDirection, normal);
            zRotation += GetRotationOffset(snapDirection,upSideDown);

            transform.rotation = Quaternion.Euler(0, 0, zRotation);
            Grounded = true;
        }
        else
        {
            transform.rotation = Quaternion.identity;
            return;
        }




    }

    public Vector2 Snap(Transform transform, Vector2 currentPosition, Vector2 mouseDeltaPosition, SnapDirection snapDirection, LayerMask lasyerMask, bool upSideDown = false)
    {

        Vector2 output = currentPosition;

        ChceckForSurface(transform, currentPosition, snapDirection, lasyerMask,upSideDown);
        if (Grounded && GetDistanceFromGround(snapDirection, currentPosition) < snapDistance + 1)
        {
            output = GroundPosition + SlideDirection * GetDeltaPosition(snapDirection, mouseDeltaPosition);
        }

        return output;
    }

    private Vector2 GetSnapDirecton(SnapDirection snapDirection)
    {
        switch (snapDirection)
        {
            case SnapDirection.left:
                return Vector2.left;
            case SnapDirection.right:
                return Vector2.right;
            case SnapDirection.up:
                return Vector2.up;
            case SnapDirection.down:
                return Vector2.down;
        }

        return Vector2.zero;
    }

    private float GetDeltaPosition(SnapDirection snapDirection, Vector2 mouseDelta)
    {
        if (snapDirection.HasFlag(SnapDirection.left) || snapDirection.HasFlag(SnapDirection.right))
        {
            return mouseDelta.y;
        }
        else
        {
            return mouseDelta.x;
        }
    }

    private float GetDistanceFromGround(SnapDirection snapDirection, Vector2 mousePosition)
    {
        if (snapDirection.HasFlag(SnapDirection.left) || snapDirection.HasFlag(SnapDirection.right))
        {
            return Mathf.Abs(GroundPosition.x - mousePosition.x);
        }
        else
        {
            return Mathf.Abs(GroundPosition.y - mousePosition.y);
        }
    }

    private float GetRotationOffset(SnapDirection snapDirection,bool upSideDown = false)
    {
        switch (snapDirection)
        {
            case SnapDirection.left:
                return upSideDown ? 90 : -90;
            case SnapDirection.right:
                return upSideDown ? -90 : 90;
            case SnapDirection.up:
                return upSideDown ? 0 : 180;
            default:
                return upSideDown ? 180 : 0;
        }
    }
}
