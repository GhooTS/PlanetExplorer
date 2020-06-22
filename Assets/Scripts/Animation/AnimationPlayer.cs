using UnityEngine;


[RequireComponent(typeof(Animation))]
public class AnimationPlayer : MonoBehaviour
{
    Animation animat;

    private void Awake()
    {
        animat = GetComponent<Animation>();
    }

    public void Play()
    {
        if (animat.isPlaying)
        {
            animat.Stop();
        }
        animat.Play();
    }
}
