using System.Collections;
using UnityEngine;


public class SwordCollider : MonoBehaviour
{
    private BoxCollider swordCollider;
    private Animator swordAnimator;
    private PlayerAttack playerAttack;
    private Sword sword;


    private void Awake()
    {
        swordCollider = GetComponent<BoxCollider>();
        swordAnimator = GetComponent<Animator>();
        playerAttack = GetComponentInParent<PlayerAttack>();
        sword = GetComponentInParent<Sword>();
        swordCollider.enabled = false;
    }
    private void Start()
    {
        playerAttack.OnPlayerAttack += HandleSwordCollider_OnPlayerAttack;
    }

    private void HandleSwordCollider_OnPlayerAttack(object sender, System.EventArgs e)
    {
        StartCoroutine(EnableBoxColliderTimer(sword.GetSwordData().attackDuration));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(sword.GetSwordData().damage);
        }
    }

    IEnumerator EnableBoxColliderTimer(float cooldown)
    {
        float swordRaiseTime = 0.2f; //todo: make this dynamic
        yield return new WaitForSeconds(swordRaiseTime);
        swordCollider.enabled = true;
        yield return new WaitForSeconds(cooldown - swordRaiseTime);
        swordCollider.enabled = false;
    }
}
