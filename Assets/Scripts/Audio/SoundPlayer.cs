using GTVariable;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    public void PlaySound(string path)
    {

        FMODUnity.RuntimeManager.PlayOneShot(path, transform.position);
    }
}
