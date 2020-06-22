using GTAttribute;
using UnityEngine;

[System.Serializable]
public class DistanceFilter
{
    [GT_MinMaxRange("Range", 0, 200)]
    public float minRange;
    [HideInInspector]
    public float maxRange;

    public bool Pass(Vector2 from, Vector2 to)
    {
        float distanceToObject = Vector2.Distance(from, to);
        return distanceToObject >= minRange && distanceToObject <= maxRange;
    }
}
