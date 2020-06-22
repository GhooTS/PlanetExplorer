using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Updater : MonoBehaviour
{
    [Range(0.5f, 60f)]
    public float updateIntervale = 1.0f;
    public bool InitialeOnStart = false;
    public bool updateOnce = false;
    public UnityEvent onUpdate;


    private WaitForSeconds waiter;

    private void Start()
    {
        waiter = new WaitForSeconds(updateIntervale);
        if (InitialeOnStart)
        {
            StartUpdating();
        }
    }

    public void StartUpdating()
    {
        StopUpdating();
        StartCoroutine(UpdateCustome());
    }

    private IEnumerator UpdateCustome()
    {
        while (true)
        {
            yield return waiter;
            onUpdate?.Invoke();
            if (updateOnce)
            {
                break;
            }
        }
    }

    public void StopUpdating()
    {
        StopAllCoroutines();
    }
}
