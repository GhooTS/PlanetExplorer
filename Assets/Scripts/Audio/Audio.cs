using GTVariable;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Audio
{
    public StringVariable name;
    public AudioSource Source { get; set; }
    public AudioMixerGroup audioMixer;
    public float playInterval = 0.1f;
    [Range(0.5f, 1.5f)]
    public float minPitch = 0.9f;
    [Range(0.5f, 1.5f)]
    public float maxPitch = 1.2f;
    [Range(0f, 1f)]
    public float volume = 1f;
    public bool stopWhenSceneChange = true;


    private float waitToTime;

    public void Play(AudioClip clip, bool loop)
    {
        Source.clip = clip;
        Source.Play();
        Source.loop = loop;
    }

    public void PlayOneShot(params AudioClip[] clips)
    {
        if (waitToTime > Time.time)
            return;


        int clipIndex = 0;
        if (clips.Length > 1)
        {
            clipIndex = Random.Range(0, clips.Length);
        }

        SetRandomPitch();
        Source.PlayOneShot(clips[clipIndex], volume);
        waitToTime = Time.time + playInterval;
    }

    public void Stop()
    {
        Source.Stop();
    }

    public void SetRandomPitch()
    {
        Source.pitch = Random.Range(minPitch, maxPitch);
    }

}

