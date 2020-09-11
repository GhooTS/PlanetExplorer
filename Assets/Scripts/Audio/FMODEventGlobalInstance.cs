using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

[CreateAssetMenu(menuName = "Audio/FMOD Global Instance")]
public class FMODEventGlobalInstance : ScriptableObject
{
    [EventRef]
    public string path;
    [NonSerialized] private EventInstance instance;
    [NonSerialized] private bool hasIntance = false;

    private void OnEnable()
    {
        hasIntance = false;
    }

    public void CreateInstance()
    {
        if (hasIntance == false)
        {
            instance = RuntimeManager.CreateInstance(path);
            hasIntance = true;
        }
    }
    

    public void Play()
    {
        if (IsPlaying() == false)
        {
            instance.start();
        }
    }

    public bool IsPlaying()
    {
        instance.getPlaybackState(out PLAYBACK_STATE state);
        return state != PLAYBACK_STATE.STOPPED;
    }


    public void Stop()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}

