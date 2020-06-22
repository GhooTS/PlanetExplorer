using UnityEngine;
using UnityEngine.Events;

public class Flipable : MonoBehaviour
{
    public SpriteRenderer[] spriteToFlip;
    public FlipableObjectBase[] flipableObjects;
    public bool facingRight = true;
    [Header("Events")]
    public UnityEvent fliped;


    public void Flip()
    {
        foreach (var sprite in spriteToFlip)
        {
            sprite.flipX = !sprite.flipX;
        }

        foreach (var flipableObject in flipableObjects)
        {
            flipableObject.FlipX = !flipableObject.FlipX;
        }
        fliped?.Invoke();
    }
}