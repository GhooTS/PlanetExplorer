using UnityEngine;
using UnityEngine.Events;

public interface IProjectileTurretSystem
{
    float BulletSpeed { get; }
    Vector3 BulletSpawnPosition { get; }
}

public interface IBeamTurretSystem
{

}

public interface IAreaTurretSystem
{

}

public interface ITurretShootSystem
{
    float BulletSpeed { get; }
    Vector3 BulletSpawnPosition { get; }

    void StartShooting();
    void StopShooting();
    void Shoot();
    bool IsReadyToShoot();
    bool NeedToReload();
}

public class TurretShootSystem : MonoBehaviour, ITurretShootSystem
{
    [Range(0, 100)]
    public float fireRate = 0.5f;
    public Transform projectileSpawnPoint;
    public Projectile projectilePrefab;
    public bool burstMode;
    public float burstFireRate;
    public float buletsInBurst;
    [Header("Events")]
    public UnityEvent shooting;
    public UnityEvent shooted;

    private ITurretAmmunitionSystem turretAmmunition;
    private int burstCounter = 0;
    private float nextShootTime = 0;

    public float BulletSpeed { get; private set; } = 0;
    public Vector3 BulletSpawnPosition { get { return projectileSpawnPoint.position; } }
    public bool IsShooting { get; private set; } = false;

    private void Start()
    {
        BulletSpeed = projectilePrefab.projectileSpeed;
        turretAmmunition = GetComponent<ITurretAmmunitionSystem>();
        if (turretAmmunition == null)
        {
            Debug.LogError("Ammunition System is requiare for this component");
        }
    }

    public void Shoot()
    {
        shooting?.Invoke();
        turretAmmunition.DecreasteAmmuntionCount(1);
        GetNextShootTime();
        Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        projectile.AddForce(projectileSpawnPoint.right);
        shooted?.Invoke();
        StopShooting();
    }

    private void GetNextShootTime()
    {
        if (burstMode)
        {
            burstCounter = turretAmmunition.HasFullAmmunition() ? 0 : burstCounter;
            burstCounter = burstCounter == buletsInBurst - 1 ? 0 : ++burstCounter;
            nextShootTime = Time.time + (burstCounter < buletsInBurst - 1 ? burstFireRate : fireRate);
        }
        else
        {
            nextShootTime = Time.time + fireRate;
        }
    }

    public bool IsReadyToShoot()
    {
        return nextShootTime < Time.time && turretAmmunition.IsReadyToShoot() && IsShooting == false;
    }

    public bool NeedToReload()
    {
        return turretAmmunition.OutOfAmmunition() && turretAmmunition.ReloadEnd();
    }

    public void StartShooting()
    {
        IsShooting = true;
    }

    public void StopShooting()
    {
        IsShooting = false;
    }

}
