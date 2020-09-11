using UnityEngine;
using FMODUnity;

[CreateAssetMenu(menuName = "Audio/Bus Volume Controller")]
public class BusVolumeController : ScriptableObject
{
    public string bussPath;
    [EventRef]
    public string sampleSound;
    public bool playSampleSound;


    public void SetVolume(float volume)
    {
        var bus = RuntimeManager.GetBus(bussPath);
        bus.setVolume(volume);
        if (playSampleSound)
        {
            RuntimeManager.PlayOneShot(sampleSound, Vector3.zero);
        }
    }

    public void Mute(bool mute)
    {
        RuntimeManager.GetBus(bussPath).setMute(mute);
    }
}

