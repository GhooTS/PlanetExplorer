using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public Audio[] audios;
    public static AudioManager current;


    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    private void Awake()
    {

        if (current == null)
        {
            current = this;
        }
        else
        {
            Debug.Log("There was more then one Audio manager");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (var audio in audios)
        {
            audio.Source = gameObject.AddComponent<AudioSource>();
            audio.Source.outputAudioMixerGroup = audio.audioMixer;
            audio.Source.volume = audio.volume;
        }
    }

    public void Play(int channelIndex, AudioClip clip, bool loop)
    {
        if (channelIndex < 0 || channelIndex > audios.Length)
        {
            Debug.LogWarning("AudioManager::index was wrong");
            return;
        }

        audios[channelIndex].Play(clip, loop);
    }

    public void SetAudioVolume(int audioIndex, float volume)
    {
        volume = Mathf.Clamp(volume, 0, 1);
        audios[audioIndex].Source.volume = volume;
    }


    public void PlayOneShot(int channelIndex, params AudioClip[] clips)
    {
        if (channelIndex < 0 || channelIndex > audios.Length)
        {
            Debug.LogWarning("AudioManager::index was wrong");
            return;
        }
        audios[channelIndex].PlayOneShot(clips);
    }


    public int GetAudioChannelByName(string name)
    {
        for (int i = 0; i < audios.Length; i++)
        {
            if (audios[i].name.value == name)
            {
                return i;
            }
        }

        //Channel was not found
        return -1;
    }

    public void OnSceneChange(Scene current, Scene loadingSceen)
    {
        foreach (var channel in audios)
        {
            if (channel.stopWhenSceneChange)
            {
                channel.Stop();
            }
        }
    }


    public bool IsPlaying(int audioIndex)
    {
        if (audioIndex < 0 || audioIndex > audios.Length)
        {
            Debug.LogWarning("AudioManager::index was wrong");
            return false;
        }

        return audios[audioIndex].Source.isPlaying;
    }
}

