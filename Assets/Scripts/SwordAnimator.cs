using UnityEngine;

public class SwordAnimator : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private Animator swordAnimator;

    private void Awake()
    {
        swordAnimator = GetComponent<Animator>();
        playerAttack = GetComponentInParent<PlayerAttack>();
    }
    private void Start()
    {
        playerAttack.OnPlayerAttack += HandleSwordAnimation_OnPlayerAttack;
    }

    private void HandleSwordAnimation_OnPlayerAttack(object sender, System.EventArgs e)
    {
        swordAnimator.SetTrigger("IsAttacking");
    }
}
