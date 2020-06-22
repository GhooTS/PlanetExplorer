using GTAttribute;
using UnityEditor;
using UnityEngine;
public class CircleGenerator : MonoBehaviour
{
    private MeshFilter meshFilter;
    [Range(0, 360)]
    public int arcStart;
    [Range(0, 360)]
    public int arcSize;
    [Range(3, 360)]
    public int resolution = 120;
    [GT_MinMaxRange("Radius", 0, 300)]
    public float innerRadius;
    [HideInInspector]
    public float outerRadius;
    Mesh circleMesh;
    private readonly CircleMeshGenerator circleMeshGenerator = new CircleMeshGenerator();
    private readonly DonutMeshGenerator donutMeshGenerator = new DonutMeshGenerator();

#if UNITY_EDITOR
    public bool enablePreview = false;
#endif



    [ContextMenu("Generate Mesh Class")]
    public void Generate()
    {
        if (meshFilter == null)
        {
            meshFilter = GetComponent<MeshFilter>();
        }

        if (circleMesh == null)
        {
            circleMesh = new Mesh();
        }


        if (innerRadius != 0)
        {
            donutMeshGenerator.GenerateMesh(circleMesh, arcStart, arcSize, innerRadius, outerRadius, resolution);
        }
        else
        {
            circleMeshGenerator.GenerateMesh(circleMesh, arcStart, arcSize, outerRadius, resolution);
        }

#if UNITY_EDITOR
        if (EditorApplication.isPlaying == false && meshFilter.sharedMesh != circleMesh)
        {
            meshFilter.sharedMesh = circleMesh;
        }
        else if (EditorApplication.isPlaying && meshFilter.mesh != circleMesh)
        {
            meshFilter.mesh = circleMesh;
        }
#else
        if (meshFilter.mesh != circleMesh)
        {
            meshFilter.mesh = circleMesh;
        }
#endif
    }


    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        if (enablePreview == false)
            return;
#endif
        GizmoExtended.DrawArc(transform.position, arcStart, arcSize, innerRadius, outerRadius, false, false, resolution);
    }
}
