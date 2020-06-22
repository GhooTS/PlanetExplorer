using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Turret : MonoBehaviour
{
    public bool PredictTargetPosition = false;
    private Animator turretAnimationController;
    private ITargetSelector targetSelector;
    private ITurretShootSystem turretShootSystem;
    private ITurretRotationSystem turretRotationSystem;
    private ITurretAmmunitionSystem turretAmmunitionSystem;

    private int shootAnimation;
    private int reloadAnimation;


    private void Start()
    {
        turretAnimationController = GetComponent<Animator>();
        turretShootSystem = GetComponent<ITurretShootSystem>();
        turretRotationSystem = GetComponent<ITurretRotationSystem>();
        targetSelector = GetComponent<ITargetSelector>();
        turretAmmunitionSystem = GetComponent<ITurretAmmunitionSystem>();

        shootAnimation = Animator.StringToHash("Shoot");
        reloadAnimation = Animator.StringToHash("Reload");
    }


    private void Update()
    {

        if (turretShootSystem.NeedToReload() && turretAnimationController.GetCurrentAnimatorStateInfo(0).shortNameHash != shootAnimation)
        {
            PlayReloadAnimation();
            turretAmmunitionSystem.StartReloading();
        }

        if (targetSelector.Target != null)
        {
            Vector3 targetPosition = PredictTargetPosition ? GetPredictedPosition() : targetSelector.Target.Position;
            turretRotationSystem.SetTarget(targetPosition);

            if (turretRotationSystem.IsAligneWithTarget(targetSelector.Target.Position - turretShootSystem.BulletSpawnPosition) && turretShootSystem.IsReadyToShoot())
            {
                PlayShootAnimation();
                turretShootSystem.StartShooting();
            }

        }
        else
        {
            if (turretRotationSystem.HasTarget())
            {
                turretRotationSystem.ClearTarget();
            }

            turretShootSystem.StopShooting();
        }
    }

    private void PlayShootAnimation()
    {
        turretAnimationController.Play(shootAnimation);
    }

    private void PlayReloadAnimation()
    {
        turretAnimationController.Play(reloadAnimation);
    }

    private Vector3 GetPredictedPosition()
    {
        return ProjectilePrediction.Predict(turretShootSystem.BulletSpeed, targetSelector.Target.EnemyVeloctiy, targetSelector.Target.Position, turretShootSystem.BulletSpawnPosition);
    }
}
