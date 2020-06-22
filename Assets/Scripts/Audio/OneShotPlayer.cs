using GTVariable;
using UnityEngine;

public class OneShotPlayer : MonoBehaviour
{
    public StringVariable channelName;
    private int channelId = -1;

    private void Start()
    {
        if (channelName == null)
        {
            Debug.LogWarning($"no channel assign for {gameObject.name}");
            return;
        }

        channelId = AudioManager.current.GetAudioChannelByName(channelName.value);
    }


    public void PlayOneShot(params AudioClip[] clips)
    {
        if (AudioManager.current == null || channelId == -1)
            return;

        AudioManager.current.PlayOneShot(channelId, clips);
    }

    public void PlayOneShot(AudioClip clip)
    {
        if (AudioManager.current == null || channelId == -1)
            return;

        AudioManager.current.PlayOneShot(channelId, clip);
    }

    public void PlayOneShot(AudioClipCollection clipCollection)
    {
        if (AudioManager.current == null || channelId == -1)
            return;

        AudioManager.current.PlayOneShot(channelId, clipCollection.clips);
    }
}

