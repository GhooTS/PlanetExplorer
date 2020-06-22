using UnityEngine;

public static class GizmoExtended
{
    public static void DrawArc(Vector3 centerPoint, float startAngle, float endAngle, float minRange, float maxRange, bool flipX = false, bool flipY = false, int resolution = 64)
    {
        //Draw start angle line
        Gizmos.DrawLine(centerPoint + GetPositionOnCircle(startAngle, minRange, flipX, flipY), centerPoint + GetPositionOnCircle(startAngle, maxRange, flipX, flipY));

        //Draw end angle line
        Gizmos.DrawLine(centerPoint + GetPositionOnCircle(startAngle + endAngle, minRange, flipX, flipY), centerPoint + GetPositionOnCircle(startAngle + endAngle, maxRange, flipX, flipY));

        DrawArcCap(centerPoint, startAngle, endAngle, minRange, resolution, flipX, flipY);
        DrawArcCap(centerPoint, startAngle, endAngle, maxRange, resolution, flipX, flipY);
    }


    public static void DrawArcCap(Vector3 centerPoint, float startAngle, float endAngle, float range, int resolution, bool flipX = false, bool flipY = false)
    {
        float stepAngle = endAngle / resolution;

        for (int i = 1; i <= resolution; i++)
        {
            Vector3 start = centerPoint + GetPositionOnCircle(startAngle + stepAngle * (i - 1), range, flipX, flipY);
            Vector3 end = centerPoint + GetPositionOnCircle(startAngle + stepAngle * i, range, flipX, flipY);
            Gizmos.DrawLine(start, end);
        }


    }

    public static Vector3 GetPositionOnCircle(float angle, float range, bool flipX = false, bool flipY = false)
    {
        float x = range * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = range * Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(flipX ? -x : x, flipY ? -y : y);
    }
}
