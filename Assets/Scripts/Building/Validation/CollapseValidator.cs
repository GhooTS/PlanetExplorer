using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CollapseValidator : MonoBehaviour, IPlacingValidator
{
    public LayerMask validationLayer;
    public string InvalideMessage { get; }
    [SerializeField]
    private Collider2D validationCollider;
    readonly RaycastHit2D[] results = new RaycastHit2D[2];
    ContactFilter2D contactFilter;

    private void Start()
    {
        contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
        contactFilter.SetLayerMask(validationLayer);
    }

    public bool Validate()
    {
        return validationCollider.Cast(Vector2.zero, contactFilter, results, 0, true) == 0;
    }
}
