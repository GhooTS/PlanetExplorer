using UnityEngine;
using UnityEngine.Events;



public class EnemyAttak : MonoBehaviour
{
    public float damage;
    public ObjectDetector playerDetector;
    public UnityEvent onAttack;
    private Health target;

    public void Start()
    {
        InvokeRepeating(nameof(LookForTarget), 0, 0.4f);
    }

    public void Attack()
    {
        if (target != null && target.Alive)
        {
            if (target.Alive)
            {
                target.DealDamage(damage);
                onAttack?.Invoke();
            }
            else
            {
                target = null;
            }
        }
    }

    public bool IsTargetInFrontOfEnemy()
    {
        return (target.transform.position.x - transform.position.x) > 0;
    }

    public bool IsTargetInRange()
    {
        return target != null;
    }

    private void LookForTarget()
    {
        target = playerDetector.GetFirstDetected<Health>();
    }




}
