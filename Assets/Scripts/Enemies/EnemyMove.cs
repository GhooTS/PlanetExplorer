using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private EnemyContoller enemyContoller;
    [Min(0)]
    public float speed;

    public void Move(bool rightDirection)
    {
        if (rightDirection)
        {
            enemyContoller.Move(speed * Time.deltaTime);
        }
        else
        {
            enemyContoller.Move(speed * Time.deltaTime * -1);
        }
    }

    public void StopMoving()
    {
        enemyContoller.Move(0);
    }
}
