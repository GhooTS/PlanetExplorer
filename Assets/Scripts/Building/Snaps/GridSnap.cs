using UnityEngine;

[CreateAssetMenu(menuName = "Ingame tools/Snaps/Grid Snap")]
public class GridSnap : ScriptableObject
{
    public bool enable;
    public Vector3 axis;
    public Vector3 axisOffset;
    public float angle;


    public Vector3 Snap(Vector3 currentPosition)
    {
        Vector3 output = currentPosition;



        output.x = GetGridPostion(output.x, axis.x);
        output.y = GetGridPostion(output.y, axis.y);
        output.z = GetGridPostion(output.z, axis.z);


        output += axisOffset;

        return output;
    }


    private float GetGridPostion(float currentPosition, float cellSize)
    {
        float output = Mathf.RoundToInt(currentPosition / cellSize) * cellSize;

        return output;
    }

}
