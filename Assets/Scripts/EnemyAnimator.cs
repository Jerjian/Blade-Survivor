using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IsWalking", Enemy.GetComponent<Enemy>().IsWalking());
    }
    private void Update()
    {
        animator.SetBool("IsWalking", Enemy.GetComponent<Enemy>().IsWalking());
    }
}
