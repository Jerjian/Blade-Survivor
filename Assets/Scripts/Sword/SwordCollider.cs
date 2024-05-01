using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    private BoxCollider swordCollider;
    private Animator swordAnimator;
    private PlayerAttack playerAttack;

    private void Awake()
    {
        swordCollider = GetComponent<BoxCollider>();
        swordAnimator = GetComponent<Animator>();
        playerAttack = GetComponentInParent<PlayerAttack>();
        swordCollider.enabled = false;
    }
    private void Start()
    {
        playerAttack.OnPlayerAttack += HandleSwordCollider_OnPlayerAttack;
    }
    private void Update()
    {
        if (swordAnimator.GetCurrentAnimatorStateInfo(0).IsName("Sword_Idle"))
        {
            Debug.Log("Sword Idle");
            swordCollider.enabled = false;
        }
    }

    private void HandleSwordCollider_OnPlayerAttack(object sender, System.EventArgs e)
    {
        Debug.Log("Sword Attack");
        swordCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit");
        }
    }
}
