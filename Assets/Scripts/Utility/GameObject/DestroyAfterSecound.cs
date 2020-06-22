using UnityEngine;

public class DestroyAfterSecound : MonoBehaviour
{
    public float liveFor;

    private void Start()
    {
        Destroy(gameObject, liveFor);
    }
}
