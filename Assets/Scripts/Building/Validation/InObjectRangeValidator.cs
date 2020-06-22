using UnityEngine;

public class InObjectRangeValidator : MonoBehaviour, IPlacingValidator
{

    public float range;
    public LayerMask objectLayer;
    public bool valideWhenInRange = true;
    public string InvalideMessage { get; }

    public bool Validate()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, range, Vector2.zero, 0, objectLayer);

        return (hit.collider != null) == valideWhenInRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

}