using UnityEngine;

public class TurretSoundPlayer : MonoBehaviour
{
    public AudioClip[] shootClips;
    public AudioClip[] reloadClips;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }



    public void PlayReloadSound()
    {
        audioSource.clip = reloadClips[Random.Range(0,reloadClips.Length - 1)];
        audioSource.Play();
    }

    public void PlayShootSound()
    {
        audioSource.clip = shootClips[Random.Range(0, shootClips.Length - 1)];
        audioSource.pitch = Random.Range(0.8f, 1.1f);
        audioSource.Play();
    }
}
