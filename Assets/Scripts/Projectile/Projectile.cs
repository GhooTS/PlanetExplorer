using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D rig2D;
    public float damage;
    public float maxLifeTime = 5.0f;
    public float projectileSpeed = 1200;
    public UnityEvent onHit;

    private bool dealedDamage = false;
    private void OnEnable()
    {
        rig2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, maxLifeTime);
    }




    public void AddForce(Vector2 direction)
    {

        rig2D.AddForce(direction * projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dealedDamage)
            return;

        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent(out Health enemy))
            {
                if (enemy.Alive)
                {
                    enemy.DealDamage(damage);
                    dealedDamage = true;
                }
                else
                {
                    return;
                }
            }
        }
        onHit?.Invoke();
        Destroy(gameObject);
    }

}
