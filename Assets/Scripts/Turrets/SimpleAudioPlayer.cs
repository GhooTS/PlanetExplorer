using UnityEngine;


public class SimpleAudioPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }

}
