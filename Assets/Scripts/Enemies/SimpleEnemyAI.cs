using UnityEngine;




public class SimpleEnemyAI : MonoBehaviour
{
    public Animator animator;
    public Transform target;
    public SpriteRenderer spriteRenderer;
    [SerializeField]
    private EnemyMove enemyMove;
    [SerializeField]
    private EnemyAttak enemyAttak;
    private bool attacking = false;

    public string coreTag = "Core";
    int attackTrigger = Animator.StringToHash("Attack");
    int moveFloat = Animator.StringToHash("Move");

    private void OnEnable()
    {
        target = GameObject.FindWithTag("Core").transform;
    }

    private void FixedUpdate()
    {
        enemyMove.StopMoving();
        if (target == null || animator.GetCurrentAnimatorStateInfo(0).fullPathHash == attackTrigger)
        {
            if (target == null)
            {
                animator.SetFloat(moveFloat, 0);
            }
            return;
        }

        if (enemyAttak.IsTargetInRange() == false)
        {
            bool moveRight = GetTargetDirection();

            enemyMove.Move(moveRight);
            spriteRenderer.flipX = !moveRight;
            animator.SetFloat(moveFloat, 1);
        }
        else
        {
            spriteRenderer.flipX = enemyAttak.IsTargetInFrontOfEnemy() ? false : true;
            animator.SetFloat(moveFloat, 0);
            animator.SetTrigger(attackTrigger);
        }
    }


    public void SetMoveTarget(Transform newTarget)
    {
        target = newTarget;
    }

    //Return true for right, false for left direction
    private bool GetTargetDirection()
    {
        Vector2 direction = target.position - transform.position;
        return direction.x > 0;
    }

}
