using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(ObjectDetector))]
public class SimpleTargetSelector : MonoBehaviour, ITargetSelector
{
    public enum EnemySelection
    {
        distance, health
    }


    public UnityEvent onTargetSelected;
    public bool keepTargetTillInRange = false;
    [SerializeField]
    private float targetUpdateTime = 0.25f;
    public EnemySelection selectEnemyBy = EnemySelection.distance;
    public bool ascending = true;

    public EnemyData Target { get; private set; }
    public float TargetSpeed { get; private set; }
    public Vector3 TargetMoveDirection { get; private set; }

    private ObjectDetector enemyDetetor;

    private void Start()
    {
        enemyDetetor = GetComponent<ObjectDetector>();
        InvokeRepeating(nameof(GetTarget), 0, targetUpdateTime);
    }

    private void GetTarget()
    {
        if (keepTargetTillInRange && enemyDetetor.Detect(Target.transform))
        {
            return;
        }

        Target = null;
        switch (selectEnemyBy)
        {
            case EnemySelection.distance:
                GetByDistance();
                break;
            case EnemySelection.health:
                GetByHealth();
                break;
        }
        if (Target != null)
        {
            onTargetSelected?.Invoke();
        }
    }

    private void GetByDistance()
    {
        float bestDistance = ascending ? Mathf.Infinity : Mathf.NegativeInfinity;
        int best = -1;

        var enemies = enemyDetetor.GetDectedObjects<EnemyData>();

        for (int i = 0; i < enemies.Count; i++)
        {
            float enemyDistanceFromGun = Vector2.Distance(enemies[i].transform.position, transform.position);
            if (enemies[i].Alive && CompartValues(bestDistance, enemyDistanceFromGun))
            {
                bestDistance = enemyDistanceFromGun;
                best = i;
            }
        }

        if (best != -1)
        {
            Target = enemies[best];
            TargetMoveDirection = enemies[best].EnemyVeloctiy.normalized;
            TargetSpeed = enemies[best].EnemyVeloctiy.magnitude;
        }
    }

    private void GetByHealth()
    {
        float bestHealth = ascending ? Mathf.Infinity : Mathf.NegativeInfinity;
        int best = -1;

        var enemies = enemyDetetor.GetDectedObjects<EnemyData>();

        for (int i = 0; i < enemies.Count; i++)
        {
            Debug.Log(enemies[i].EnemyCurrentHealth);
            if (enemies[i].Alive && CompartValues(bestHealth, enemies[i].EnemyCurrentHealth))
            {
                bestHealth = enemies[i].EnemyCurrentHealth;
                best = i;

            }
        }

        if (best != -1)
        {
            Debug.Log(best);
            Target = enemies[best];
            TargetMoveDirection = enemies[best].EnemyVeloctiy.normalized;
            TargetSpeed = enemies[best].EnemyVeloctiy.magnitude;
        }
    }


    private bool CompartValues(float current, float newValue)
    {
        return ascending ? current > newValue : current < newValue;
    }


    private void OnDrawGizmos()
    {
        if (Target == null) return;
        Gizmos.DrawLine(transform.position, Target.Position);
    }
}
