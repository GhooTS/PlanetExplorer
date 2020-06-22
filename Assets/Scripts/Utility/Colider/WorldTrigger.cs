using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class WorldTrigger : MonoBehaviour
{
    public string interactWith;
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;


    protected virtual void TrigerEnterAction(Collider2D other)
    {

    }

    protected virtual void TrigerExitAction(Collider2D other)
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (interactWith == "" || other.CompareTag(interactWith))
        {
            onTriggerEnter?.Invoke();
            TrigerEnterAction(other);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (interactWith == "" || other.CompareTag(interactWith))
        {
            onTriggerExit?.Invoke();
            TrigerExitAction(other);
        }
    }

}
