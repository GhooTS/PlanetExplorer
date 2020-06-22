using GTVariable;
using System.Collections;
using UnityEngine;

public class SteadyPlayer : MonoBehaviour
{
    public StringVariable channelName;
    public AudioClip clip;
    public bool loop;
    public bool playOnStart;
    public float audioTransitionTime;
    public AnimationCurve transitioCurve;
    private int channelId = -1;

    private void Start()
    {
        channelId = AudioManager.current.GetAudioChannelByName(channelName.value);
        if (playOnStart)
        {
            Play();
        }
    }

    public void Play()
    {
        AudioManager.current.Play(channelId, clip, loop);
    }

    public void PlayWithTransition(AudioClip transitionClip)
    {
        StopAllCoroutines();
        StartCoroutine(Translate(transitionClip));
    }

    private IEnumerator Translate(AudioClip transitionClip)
    {
        if (channelId != -1)
        {
            float time = audioTransitionTime;
            float halfTransition = audioTransitionTime / 2;
            float volume = transitioCurve.Evaluate(0);
            while (time > 0)
            {
                time -= Time.unscaledDeltaTime;
                time = Mathf.Max(time, 0);
                volume = transitioCurve.Evaluate(1 - (time / halfTransition));
                SetVolume(volume);
                yield return null;
            }

            AudioManager.current.Play(channelId, transitionClip, loop);
            while (time < halfTransition)
            {
                time += Time.unscaledDeltaTime;
                time = Mathf.Min(time, audioTransitionTime);
                volume = transitioCurve.Evaluate(1 - (time / halfTransition));
                SetVolume(volume);
                yield return null;
            }
        }
    }


    public void SetVolume(float volume)
    {
        AudioManager.current.SetAudioVolume(channelId, volume);
    }

    public void Play(AudioClip clip)
    {
        AudioManager.current.Play(channelId, clip, loop);
    }

}

