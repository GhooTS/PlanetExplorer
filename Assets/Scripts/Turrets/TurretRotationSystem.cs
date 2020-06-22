using UnityEngine;

public interface ITurretRotationSystem
{
    bool HasTarget();
    void SetTarget(Transform target);
    void SetTarget(Vector3 location);
    void ClearTarget();
    bool IsAligneWithTarget(Vector3 targetDirection);
}

[System.Serializable]
public class TurretRotationSystem : MonoBehaviour, ITurretRotationSystem
{

    public Transform turretRotator;
    [Min(0)]
    public float rotationSpeed;
    public bool disableTargetAlignment;
    [Range(0, 180)]
    public float angleRequiareToShoot;
    public LookAtObject2D.FacingDirection facingDirection;

    private Vector3 targetLocation;
    private Transform target;

    private void Start()
    {
        targetLocation = turretRotator.position + (Vector3)GetFacingDirection();
    }

    private void Update()
    {
        Rotate(targetLocation);
    }

    private void Rotate(Vector3 targetPosition)
    {
        if (rotationSpeed == 0)
        {
            return;
        }
        Quaternion desireRotation = turretRotator.GetLookAtRotation(targetPosition, facingDirection);
        turretRotator.rotation = Quaternion.Lerp(turretRotator.rotation, desireRotation, Time.deltaTime * rotationSpeed);
    }

    public bool IsAligneWithTarget(Vector3 targetDirection)
    {
        return disableTargetAlignment || Vector2.Angle(GetFacingDirection(), targetDirection) <= angleRequiareToShoot;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetTarget(Vector3 location)
    {
        targetLocation = location;
    }

    public void ClearTarget()
    {
        target = null;
    }

    public bool HasTarget()
    {
        return target != null;
    }

    public void RevereseFacingDirection()
    {
        switch (facingDirection)
        {
            case LookAtObject2D.FacingDirection.right:
                facingDirection = LookAtObject2D.FacingDirection.left;
                break;
            case LookAtObject2D.FacingDirection.left:
                facingDirection = LookAtObject2D.FacingDirection.right;
                break;
            case LookAtObject2D.FacingDirection.up:
                facingDirection = LookAtObject2D.FacingDirection.down;
                break;
            case LookAtObject2D.FacingDirection.down:
                facingDirection = LookAtObject2D.FacingDirection.up;
                break;
        }
    }

    private Vector2 GetFacingDirection()
    {
        switch (facingDirection)
        {
            case LookAtObject2D.FacingDirection.right:
                return turretRotator.right;
            case LookAtObject2D.FacingDirection.left:
                return -turretRotator.right;
            case LookAtObject2D.FacingDirection.up:
                return turretRotator.up;
            case LookAtObject2D.FacingDirection.down:
                return -turretRotator.up;
            default:
                return turretRotator.right;
        }


    }

}
