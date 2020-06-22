using UnityEngine;

public class FaceObjectDirection : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Transform target;
    public bool spriteFaceRight = true;
    public bool faceObject = true;
    public string targetTag;

    private void Start()
    {
        if (targetTag.Length != 0)
        {
            target = GameObject.FindWithTag(targetTag).transform;
        }
    }


    private void Update()
    {
        if (target == null)
            return;

        if (transform.position.x < target.position.x)
        {
            sprite.flipX = faceObject != spriteFaceRight;
        }
        else
        {
            sprite.flipX = !faceObject != spriteFaceRight;
        }
    }
}
