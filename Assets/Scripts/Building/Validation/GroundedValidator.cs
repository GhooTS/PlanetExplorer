using UnityEngine;

public class GroundedValidator : MonoBehaviour, IPlacingValidator
{
    private enum GroundDirection
    {
        up, down, left, right
    }

    public string InvalideMessage { get; }
    [SerializeField]
    private GroundDirection groundDirection;
    public float rayDistance = 1f;
    public LayerMask checkLayer;
    

    public bool Validate()
    {
        return Physics2D.Raycast(transform.position, GetDirection(),rayDistance, checkLayer).collider;
    }

    private Vector2 GetDirection()
    {
        switch (groundDirection)
        {
            case GroundDirection.up:
                return Vector2.up;
            case GroundDirection.down:
                return Vector2.down;
            case GroundDirection.left:
                return Vector2.left;
            case GroundDirection.right:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }
}
