using UnityEngine;

public class RangeShaderController : MonoBehaviour
{ 
    public Color circleColor;
    public float innerRadius;
    public float outerRadius;
    [Range(-360, 360)]
    public float arcStrat = 0;
    [Range(0, 360)]
    public float fill = 360;
    private Material rangeDisplayMat;
    private Mesh rangeMesh;

    private void Start()
    {
        rangeDisplayMat = GetComponent<Renderer>().material;
        rangeMesh = GetComponent<MeshFilter>().mesh;
        GetComponent<MeshRenderer>().sortingLayerName = "Projectile";
    }

    private void Update()
    {
        UpdateShader();
        UpdateMesh();
    }

    public void UpdateShader()
    {
        rangeDisplayMat.SetColor("CircleColor", circleColor);
        rangeDisplayMat.SetFloat("InnerRadius", innerRadius);
        rangeDisplayMat.SetFloat("OuterRadius", outerRadius);
        rangeDisplayMat.SetFloat("ArcStart", arcStrat);
        rangeDisplayMat.SetFloat("Fill", fill);
    }


    [ContextMenu("UpdateRange")]
    public void UpdateMesh()
    {
        Bounds newBounds = rangeMesh.bounds;
        newBounds.extents = new Vector3(outerRadius, outerRadius, 0);
        rangeMesh.bounds = newBounds;
    }
}
