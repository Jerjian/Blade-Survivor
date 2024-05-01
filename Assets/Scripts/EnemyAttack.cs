using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHand;
    private Player player;
    private Animator animator;
    private Enemy enemy;
    private float attackRange;
    private BoxCollider rightHandCollider;
    private BoxCollider leftHandCollider;
    private int damage;

    public event EventHandler OnPlayerHit;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy = GetComponent<Enemy>();
        enemy.OnAttack += HandleAttack_OnAttack;
        OnPlayerHit += PrintPlayerHit_OnPlayerHit;

        attackRange = enemy.GetAttackRange();
        animator = GetComponentInChildren<Animator>();
        rightHandCollider = rightHand.GetComponent<BoxCollider>();
        leftHandCollider = leftHand.GetComponent<BoxCollider>();

        damage = 10;
    }

    private void HandleAttack_OnAttack(object sender, System.EventArgs e)
    {
        float animationTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;

        if (animationTime > 0.27f && animationTime < 0.63f)
        {
            leftHandCollider.enabled = true;
            rightHandCollider.enabled = false;
        }
        else if (animationTime > 0.63f)
        {
            rightHandCollider.enabled = true;
            leftHandCollider.enabled = false;
        }
        else
        {
            rightHandCollider.enabled = false;
            leftHandCollider.enabled = false;
        }
    }

    private void PrintPlayerHit_OnPlayerHit(object sender, System.EventArgs e)
    {
        Debug.Log("Player Hit");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.TakeDamage(damage);
            if (OnPlayerHit != null) OnPlayerHit(this, EventArgs.Empty); //fire event OnPlayerHit
        }
    }
}
