using GTAttribute;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MeshSortingLayer : MonoBehaviour
{
    public int sortingOrder = 0;
    public string sortingLayerName;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        
        UpdateMesh();
    }


    [ContextMenu("Update")]
    public void UpdateMesh()
    {
        if(meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.sortingOrder = sortingOrder;
        meshRenderer.sortingLayerName = sortingLayerName;

        
    }
}
