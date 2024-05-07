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
            //Enemy takes damage
            other.GetComponent<Enemy>().TakeDamage(sword.GetSwordData().damage);
        }
    }

    IEnumerator EnableBoxColliderTimer(float cooldown)
    {
        swordCollider.enabled = true;
        yield return new WaitForSeconds(cooldown);
        swordCollider.enabled = false;
    }
}
