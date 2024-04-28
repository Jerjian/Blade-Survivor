using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    //[SerializeField] private GameObject Enemy;
    [SerializeField] private Enemy Enemy;
    private Player Player;
    private Animator animator;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        Enemy.OnAttack += HandleAttackAnimation_OnAttack;
        Enemy.OnWalking += HandleWalkingAnimation_OnWalking;
        Enemy.OnIdle += HandleIdleAnimation_OnIdle;
        Player.OnPlayerDeath += HandleIdleAnimation_OnPlayerDeath;
    }

    private void HandleIdleAnimation_OnPlayerDeath(object sender, System.EventArgs e)
    {
        animator.SetTrigger("IsIdle");
    }
    private void HandleAttackAnimation_OnAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger("IsAttacking");
    }
    private void HandleWalkingAnimation_OnWalking(object sender, System.EventArgs e)
    {
        animator.SetTrigger("IsWalking");
    }
    private void HandleIdleAnimation_OnIdle(object sender, System.EventArgs e)
    {
        animator.SetTrigger("IsIdle");
    }
}
