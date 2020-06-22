using System.Collections;
using UnityEngine;

public class ColliderResizer : MonoBehaviour
{
    public CircleCollider2D colliderToResize;
    public float size;
    public float resizeTime;
    public float resizeDealay;
    public AnimationCurve curve;
    public bool startResizeOnEnable;

    private void OnEnable()
    {
        if (startResizeOnEnable)
        {
            StartResize();
        }
    }

    [ContextMenu("resizeTest")]
    public void StartResize()
    {
        StopResize();
        StartCoroutine(Resize());
    }

    public void StopResize()
    {
        StopAllCoroutines();
    }

    private IEnumerator Resize()
    {
        float time = 0;

        colliderToResize.radius = 0;

        yield return new WaitForSeconds(resizeDealay);

        colliderToResize.radius = curve.Evaluate(time / resizeTime) * size;
        yield return null;

        while (time < resizeTime)
        {
            time += Time.deltaTime;
            time = Mathf.Min(time, resizeTime);
            colliderToResize.radius = curve.Evaluate(time / resizeTime) * size;
            yield return null;
        }

    }

}
