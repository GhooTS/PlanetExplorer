using UnityEngine;

[RequireComponent(typeof(Health), typeof(Rigidbody2D))]
public class EnemyData : MonoBehaviour
{
    public Vector3 EnemyVeloctiy { get { return enemyRig2d.velocity; } }
    public float EnemyCurrentHealth
    {
        get
        {
            if (EnemyHP == null)
            {
                return 0;
            }
            else
            {
                return EnemyHP.GetCurrentHP();
            }
        }
    }
    public Vector3 Position { get { return transform.position; } }
    public bool Alive { get { return EnemyHP != null && EnemyHP.Alive; } }
    public Health EnemyHP { get; private set; }
    private Rigidbody2D enemyRig2d;

    private void Start()
    {
        EnemyHP = GetComponent<Health>();
        enemyRig2d = GetComponent<Rigidbody2D>();
    }
}
