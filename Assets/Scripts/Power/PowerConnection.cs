using UnityEngine;

public class PowerConnection : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform source;
    public Transform target;


    private void Start()
    {

    }


    public void Connect(Transform target)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, source.position);
        lineRenderer.SetPosition(1, target.position);
    }

    public void Clear()
    {
        lineRenderer.positionCount = 0;
    }
}
