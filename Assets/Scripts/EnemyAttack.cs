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


    }

    private void HandleAttack_OnAttack(object sender, System.EventArgs e)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.27f && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.63f)
        {
            leftHandCollider.enabled = true;
            rightHandCollider.enabled = false;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.63f)
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
        player.TakeDamage(10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (OnPlayerHit != null) OnPlayerHit(this, EventArgs.Empty); //fire event OnPlayerHit
        }
    }
}
