using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Sword sword;

    public event EventHandler OnPlayerAttack;
    private bool canAttack = true;



    private void Start()
    {
        OnPlayerAttack += PlayerAttack_OnPlayerAttack;
        sword = GetComponentInChildren<Sword>();
    }

    private void Update()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {
        if (gameInput.GetAttackInput() && canAttack)
        {
            OnPlayerAttack?.Invoke(this, EventArgs.Empty);
        }
    }

    private void PlayerAttack_OnPlayerAttack(object sender, EventArgs e)
    {
        StartCoroutine(AttackCooldownRoutine(sword.GetSwordData().attackCooldown));
    }


    IEnumerator AttackCooldownRoutine(float cooldown)
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

}
