using UnityEngine;
using FMODUnity;

public class FMODGlobalInstanceTrigger : MonoBehaviour
{
    public FMODEventGlobalInstance musicController;

    private void OnEnable()
    {
        musicController.CreateInstance();
        musicController.Play();
    }
}

