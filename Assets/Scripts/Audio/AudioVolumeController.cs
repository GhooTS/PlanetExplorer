using UnityEngine;
using UnityEngine.Audio;

public class AudioVolumeController : MonoBehaviour
{
    [Range(-80, 20)]
    public float maxVolume;
    [Range(-80, 20)]
    public float minVolume;
    public string musicVolumeParam;
    public AudioMixer musicMixer;
    public string SFXVolumeParam;
    public AudioMixer SFXMixer;

    public void SetMusicVolume(float volume)
    {
        float range = 0;
        if (minVolume < 0 && maxVolume > 0)
        {
            range = Mathf.Abs(minVolume) + maxVolume;
        }
        else
        {
            range = Mathf.Abs(maxVolume) - Mathf.Abs(minVolume);
        }

        float value = minVolume + (range * volume);
        if (value == minVolume)
        {
            musicMixer.SetFloat(musicVolumeParam, -80);
        }
        else
        {
            musicMixer.SetFloat(musicVolumeParam, value);
        }
    }

    public void SetSFXVolume(float volume)
    {
        float range = 0;
        if (minVolume < 0 && maxVolume > 0)
        {
            range = Mathf.Abs(minVolume) + maxVolume;
        }
        else
        {
            range = Mathf.Abs(maxVolume) - Mathf.Abs(minVolume);
        }

        float value = minVolume + (range * volume);
        if (value == minVolume)
        {
            SFXMixer.SetFloat(SFXVolumeParam, -80);
        }
        else
        {
            SFXMixer.SetFloat(SFXVolumeParam, value);
        }
    }
}

