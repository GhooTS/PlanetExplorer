using UnityEngine;
using UnityEngine.Events;

public class Powerable : MonoBehaviour
{
    public float powerRequire;
    public bool hasPower;
    public int numberOfPowers = 0;


    private void Start()
    {
        if (hasPower)
        {
            onPowerOn?.Invoke();
        }
        else
        {
            onPowerOff?.Invoke();
        }
    }

    public UnityEvent onPowerOn;
    public UnityEvent onPowerOff;



    public void TurnPowerOn()
    {
        numberOfPowers += 1;

        if (hasPower)
            return;

        hasPower = true;
        onPowerOn?.Invoke();
    }

    public void TurnPowerOff()
    {
        numberOfPowers -= 1;

        if (hasPower == false || numberOfPowers > 0)
            return;

        hasPower = false;
        onPowerOff?.Invoke();
    }
}
