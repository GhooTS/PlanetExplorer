using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BeamRenderer
{
    public float speed;
    [Range(0, 360)]
    public float angleStep;
    [Range(0, 20)]
    public float sinSize;
    [Range(0, 20)]
    public float intervale;
    private float angle;



    public void SetBeamPosition(LineRenderer beam, Vector2 direction, float distance, Vector2 begin, Vector2 end)
    {

        beam.positionCount = Mathf.Max(2, (int)(distance / intervale));

        if (beam.positionCount == 0)
        {
            return;
        }

        beam.SetPosition(0, begin);
        angle += speed * Time.deltaTime;
        angle = angle % 360;
        Vector2 up = new Vector2(-direction.y, direction.x);
        for (int i = 1; i < beam.positionCount - 1; i++)
        {
            Vector2 pointPosition = begin + (direction * i / beam.positionCount * distance);
            pointPosition += up * Mathf.Sin(angle + angleStep * i) * sinSize;
            beam.SetPosition(i, pointPosition);
        }
        beam.SetPosition(beam.positionCount - 1, end);
    }
}

public class BeamTurretShootSystem : MonoBehaviour, ITurretShootSystem
{

    public float damage;
    public float damageDealInterval;
    public LineRenderer beam;
    public Transform beamStart;
    public BeamRenderer beamRenderer;
    ITargetSelector targetSelector;
    ITurretRotationSystem rotationSystem;

    [Header("Events")]
    public UnityEvent damaged;

    public float BulletSpeed { get { return 0; } }
    public Vector3 BulletSpawnPosition { get { return beamStart.position; } }

    private float nextDamageTime;
    private float angle;
    private float speed = 30;

    private void Start()
    {
        targetSelector = GetComponent<ITargetSelector>();
        rotationSystem = GetComponent<ITurretRotationSystem>();
        beam.useWorldSpace = true;
        beam.positionCount = 2;
        nextDamageTime = 0;
    }

    public void StartShooting()
    {

    }

    private void LateUpdate()
    {

        if (targetSelector.Target != null && targetSelector.Target.Alive)
        {
            Vector3 targetDirection = (targetSelector.Target.Position - beamStart.position).normalized;
            if (rotationSystem.IsAligneWithTarget(targetDirection) == false)
            {
                beam.positionCount = 0;
                return;
            }

            float distance = Vector2.Distance(targetSelector.Target.Position, beamStart.position);

            beamRenderer.SetBeamPosition(beam, targetDirection, distance, beamStart.position, targetSelector.Target.Position);

            if (nextDamageTime < Time.time)
            {
                targetSelector.Target.EnemyHP.DealDamage(damage);
                damaged?.Invoke();
                nextDamageTime = Time.time + damageDealInterval;
            }
        }
        else
        {
            beam.positionCount = 0;
        }
    }

    private IEnumerator RenderBeam(Health target)
    {
        float nextDamageDealTime = damageDealInterval;
        beam.positionCount = 2;
        while (target.Alive)
        {
            beam.SetPosition(0, beamStart.position);
            beam.SetPosition(1, target.transform.position);
            nextDamageDealTime -= Time.deltaTime;

            if (nextDamageDealTime <= 0)
            {
                target.DealDamage(damage);
                damaged?.Invoke();
                nextDamageDealTime = damageDealInterval;
            }

            yield return null;
        }
    }

    public void StopShooting()
    {
        StopAllCoroutines();
    }

    public void Shoot()
    {

    }

    public bool IsReadyToShoot()
    {
        return true;
    }

    public bool NeedToReload()
    {
        return false;
    }
}
