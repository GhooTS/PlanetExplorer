using UnityEngine;
using UnityEngine.Events;


public class KeyTrigger : MonoBehaviour
{
    public KeyCode triggerKey;
    public UnityEvent onKeyPressed;


    private void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            onKeyPressed?.Invoke();
        }
    }
}
