using GTVariable;
using UnityEngine;

public class ResourceRewarder : MonoBehaviour
{
    public FloatVariable playerResource;
    public int reward;

    public void RewardPlayer()
    {
        playerResource.SetValueWithEvent(playerResource.value += reward);
    }
}
