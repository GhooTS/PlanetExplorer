using UnityEngine;

[CreateAssetMenu(menuName = "MoveData")]
public class MoveData : ScriptableObject
{
    public enum MoveMode
    {
        free, snapToGrid, snapToGround, snapToGroundAndGrid
    }



    public MoveMode moveMode;
    public SurfaceSnap.SnapDirection snapDirection;
    public LayerMask groundLayer;
    public bool upSideDown = false;
}
