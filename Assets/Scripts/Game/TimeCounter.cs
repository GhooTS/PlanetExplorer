using GTVariable;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField]
    private FloatVariable playTime;
    private float startTime;

    private void Start()
    {
        playTime.value = 0;
    }

    public void SetStartTime()
    {
        startTime = Time.time;
    }

    public void SumUpTime()
    {
        playTime.value += Time.time - startTime;
        SetStartTime();
    }
}
