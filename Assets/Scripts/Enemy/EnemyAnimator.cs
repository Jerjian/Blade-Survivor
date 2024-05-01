using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Enemy Enemy;
    private Player Player;
    private Animator enemyAnimator;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemyAnimator = GetComponent<Animator>();
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
        enemyAnimator.SetTrigger("IsIdle");
    }
    private void HandleAttackAnimation_OnAttack(object sender, System.EventArgs e)
    {
        enemyAnimator.SetTrigger("IsAttacking");
    }
    private void HandleWalkingAnimation_OnWalking(object sender, System.EventArgs e)
    {
        enemyAnimator.SetTrigger("IsWalking");
    }
    private void HandleIdleAnimation_OnIdle(object sender, System.EventArgs e)
    {
        enemyAnimator.SetTrigger("IsIdle");
    }
}
