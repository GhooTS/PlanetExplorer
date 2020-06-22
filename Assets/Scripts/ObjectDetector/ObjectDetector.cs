using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [Header("Detection Options")]
    [SerializeField]
    private Transform sensorTransform;
    [SerializeField]
    private LayerMask detectionLayer;
    [Range(1, 128)]
    [SerializeField]
    private int maxDetectedObjects = 16;

    [Header("Filters")]
    public bool filterByVisibility;
    [SerializeField]
    private VisabilityFilter visabilityChecker;
    public bool filterByTag;
    [SerializeField]
    private TagFilter tagFilter;
    public bool filterByArc;
    [SerializeField]
    private ArcFilter arcFilter;

    //Arc Filter properties
    public float ArcStart
    {
        get { return arcFilter.startArc; }
        set { arcFilter.startArc = value; }
    }

    public float ArcSize
    {
        get { return arcFilter.arcSize; }
        set { arcFilter.arcSize = value; }
    }

    public float CurrentArcStartAngle
    {
        get { return arcFilter.CurrentArcStartAngle; }
    }

    public Vector2 ArcCenterVector
    {
        get
        {
            return arcFilter.ArcCenterVector;
        }
    }

    public bool ArcFlipX
    {
        get { return arcFilter.flipX; }
        set { arcFilter.flipX = value; }
    }

    public bool ArcFlipY
    {
        get { return arcFilter.flipY; }
        set { arcFilter.flipY = value; }
    }

    public bool ArcOffsetByZRotation
    {
        get { return arcFilter.offsetByZRotation; }
        set { arcFilter.offsetByZRotation = value; }
    }

    [SerializeField]
    private DistanceFilter distanceFilter;

    //Distance Filter properties
    public float MinRange
    {
        get { return distanceFilter.minRange; }
        set { distanceFilter.minRange = value; }
    }
    public float MaxRange
    {
        get { return distanceFilter.maxRange; }
        set { distanceFilter.maxRange = value; }
    }




    //Private fields
    private RaycastHit2D[] hitBuffer;

    private void Start()
    {
        hitBuffer = new RaycastHit2D[maxDetectedObjects];
        arcFilter.offsetTransform = transform;
        arcFilter.UpdateArcCenterVector();
    }

    public int GetBestObject<T>(List<T> objectsToCheck, Func<T, T, bool> comparator)
        where T : MonoBehaviour
    {

        if (objectsToCheck.Count == 0)
        {
            return -1;
        }

        if (objectsToCheck.Count == 1)
        {
            return 0;
        }

        int best = 0;

        for (int i = 1; i < objectsToCheck.Count; i++)
        {
            if (Detect(objectsToCheck[i].transform) && (comparator != null || comparator(objectsToCheck[best], objectsToCheck[i])))
            {
                best = i;
            }
        }

        return best;
    }

    public List<T> GetDectedObjects<T>()
        where T : MonoBehaviour
    {
        List<T> output = new List<T>();
        hitBuffer = Physics2D.CircleCastAll(transform.position, MaxRange, Vector2.zero, MaxRange, detectionLayer);

        foreach (var hit in hitBuffer)
        {
            if (Detect(hit.transform) && hit.transform.TryGetComponent(out T detectedObject))
            {
                output.Add(detectedObject);
            }
        }


        return output;
    }

    public T GetFirstDetected<T>()
        where T : MonoBehaviour
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, MaxRange, Vector2.zero, MaxRange, detectionLayer);

        if (hit.collider != null)
        {

            if (Detect(hit.transform) && hit.transform.TryGetComponent(out T detectedObject))
            {
                return detectedObject;
            }
        }

        return null;
    }

    public bool Detect(Transform objectTofilter)
    {
        return Detect(objectTofilter, filterByTag, true, filterByArc, filterByVisibility);
    }

    public bool Detect(Transform objectTofilter, bool filterByTag, bool filterByDistance, bool filterByArc, bool filterByVisibility)
    {
        if (filterByTag && tagFilter.Pass(objectTofilter) == false)
        {
            return false;
        }

        if (filterByDistance && distanceFilter.Pass(sensorTransform.position, objectTofilter.position) == false)
        {
            return false;
        }

        if (filterByArc && arcFilter.Pass(sensorTransform.position, objectTofilter.position) == false)
        {
            return false;
        }

        if (filterByVisibility && visabilityChecker.Pass(objectTofilter.position) == false)
        {
            return false;
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        if (sensorTransform == null)
            return;

        if (filterByArc)
        {
            if (arcFilter.offsetTransform == null)
            {
                arcFilter.offsetTransform = transform;
            }

            //Draw arc
            GizmoExtended.DrawArc(sensorTransform.position, arcFilter.ArcStartAngle, arcFilter.arcSize, MinRange, MaxRange, ArcFlipX, ArcFlipY);
        }
        else
        {
            GizmoExtended.DrawArc(sensorTransform.position, 0, 360, MinRange, MaxRange);
        }
    }
}