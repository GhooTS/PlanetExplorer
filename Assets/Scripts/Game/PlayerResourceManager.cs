using GTVariable;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    [SerializeField]
    private FloatVariable playerResource;
    [Min(0)]
    [SerializeField]
    private int startResource;
    [Min(0)]
    [SerializeField]
    private int waveClearReward;


    private void Start()
    {
        playerResource.SetValueWithEvent(startResource);
    }


    public void GiveWaveClearReward()
    {
        playerResource.SetValueWithEvent(playerResource.value + waveClearReward);
    }
}
