using UnityEngine;
using UnityEngine.Animations;

public static class Vector3Extension
{
    public static float GetVectorPosition(this Vector3 position, Axis axis)
    {
        switch (axis)
        {
            case Axis.X:
                return position.x;
            case Axis.Y:
                return position.y;
            case Axis.Z:
                return position.z;
        }

        return 0;
    }

    public static Vector3 SetVectorPostion(this Vector3 position, Axis axis, float value)
    {
        switch (axis)
        {
            case Axis.X:
                position.x = value;
                return position;
            case Axis.Y:
                position.y = value;
                return position;
            case Axis.Z:
                position.z = value;
                return position;
        }

        return position;
    }
}
