using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    //[SerializeField] private GameObject Enemy;
    [SerializeField] private Enemy Enemy;
    private Player player;


    private Animator animator;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsWalking", Enemy.IsWalking());
    }
    private void Start()
    {
        Enemy.OnAttack += HandleAttackAnimation_OnAttack;
        Enemy.OnWalking += HandleWalkingAnimation_OnWalking;
    }
    private void Update()
    {
        if (!player) animator.SetTrigger("IsIdle");
    }

    private void HandleAttackAnimation_OnAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger("IsAttacking");
    }
    private void HandleWalkingAnimation_OnWalking(object sender, System.EventArgs e)
    {
        animator.SetTrigger("IsWalking");
    }

}
