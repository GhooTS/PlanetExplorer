using UnityEngine;

public class HitParticleSpawner : MonoBehaviour
{
    public ParticleSystem hitParticle;
    public ContactFilter2D contactFilter;
    private Collider2D hitCollider;
    private RaycastHit2D[] hits = new RaycastHit2D[1];


    private void Start()
    {
        hitCollider = GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        int hitsCount = hitCollider.Cast(Vector2.one, hits);
        if (hitsCount > 0)
        {
            float angle = Vector2.Angle(Vector2.up, hits[0].normal);
            Instantiate(hitParticle, hits[0].point, Quaternion.Euler(0, 0, angle));
        }
    }
}
