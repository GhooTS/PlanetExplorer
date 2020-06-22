using UnityEngine;

[System.Serializable]
public class VisabilityFilter
{
    public LayerMask obstractionLayer;
    public Transform visabilityStartPoint;

    public bool Pass(Vector3 objectPosition)
    {
        var distance = Vector2.Distance(visabilityStartPoint.position, objectPosition);
        var direction = objectPosition - visabilityStartPoint.position;
        return Pass(distance, direction);
    }

    public bool Pass(Vector3 objectPosition, float distance)
    {
        var direction = objectPosition - visabilityStartPoint.position;
        return Pass(distance, direction);
    }

    public bool Pass(float distance, Vector2 direction)
    {
        return Physics2D.Raycast(visabilityStartPoint.position, direction, distance, obstractionLayer).collider == null;
    }
}
