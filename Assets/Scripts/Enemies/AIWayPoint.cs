using UnityEngine;
public class AIWayPoint : MonoBehaviour
{
    [SerializeField]
    private Transform nextWayPoint;

    public Transform GetNextWayPoint()
    {
        return nextWayPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<SimpleEnemyAI>().SetMoveTarget(nextWayPoint);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (nextWayPoint == null)
        {
            return;
        }
        Gizmos.DrawLine(transform.position, nextWayPoint.position);
    }
}
