using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    //[SerializeField] private GameObject Enemy;
    [SerializeField] private Enemy Enemy;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IsWalking", Enemy.IsWalking());
    }
    private void Update()
    {
        if (Enemy.InAttackingRange())
        {
            animator.SetBool("IsAttacking", Enemy.IsAttacking());
        }
        else
        {
            animator.SetBool("IsAttacking", Enemy.IsAttacking());
            animator.SetBool("IsWalking", Enemy.IsWalking());
        }
    }
}
