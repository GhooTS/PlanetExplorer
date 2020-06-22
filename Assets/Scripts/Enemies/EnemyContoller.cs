using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyContoller : MonoBehaviour
{
    [SerializeField]
    private float groundDistance;
    [SerializeField]
    private LayerMask standOn;
    private Rigidbody2D rig2D;
    private Vector2 lastNormal;

    private void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        lastNormal = Vector2.up;
    }




    public void Move(float speed)
    {
        if (speed == 0)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -lastNormal, groundDistance, standOn);

        if (hit.collider != null)
        {
            Vector3 moveAlong = new Vector2(hit.normal.y, -hit.normal.x);
            rig2D.velocity = moveAlong * speed;
            lastNormal = hit.normal;
        }
        else
        {
            lastNormal = Vector2.up;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundDistance);
    }
}
