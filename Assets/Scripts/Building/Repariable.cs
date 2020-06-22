using GTVariable;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Repariable : MonoBehaviour
{
    public FloatReference repaireTime;
    [SerializeField]
    private Health health;

    [Header("Events")]
    public UnityEvent reparing;
    public UnityEvent repared;

    public void Repaire()
    {
        reparing?.Invoke();
        StopAllCoroutines();
        StartCoroutine(ProcedeRepairing());
    }

    public void RepaireInstantile()
    {
        health.Heal(health.MaxHP.Value);
    }


    private IEnumerator ProcedeRepairing()
    {
        float amountToRepaire = health.MaxHP.Value - health.GetCurrentHP();
        float oneHealValue = health.MaxHP.Value / repaireTime.Value;


        while (amountToRepaire > 0)
        {
            var thisFrameHeal = oneHealValue * Time.deltaTime;
            amountToRepaire -= thisFrameHeal;
            health.Heal(thisFrameHeal);
            yield return null;
        }
        repared?.Invoke();
    }
}
