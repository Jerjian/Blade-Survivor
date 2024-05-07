using Cinemachine.Utility;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float attackRange = 2f;


    private GameObject PlayerGO;
    private bool isWalking = false;
    private Vector3 playerPosition;
    private Vector3 enemyPosition;
    private bool isAttacking = false;
    private bool randomSide;
    private bool isDead;
    private bool canBeDamaged = true;


    public event EventHandler OnAttack;
    public event EventHandler OnWalking;
    public event EventHandler OnIdle;
    public event EventHandler<float> OnHealthChanged;
    public event EventHandler OnEnemyDeath;

    private void Awake()
    {
        isWalking = false;
        randomSide = UnityEngine.Random.Range(0, 2) == 0;
        PlayerGO = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
    }
    private void Update()
    {
        //Debug.Log("Enemy Update");
        if (PlayerGO) playerPosition = PlayerGO.transform.position;
        enemyPosition = transform.position;
        float distanceToPlayer = Vector3.Distance(playerPosition, enemyPosition);

        if (PlayerGO != null)
        {
            if (distanceToPlayer > attackRange) HandleMovement();
            else HandleAttack();
        }
        else HandleIdle();
    }
    private void HandleMovement()
    {
        Vector3 moveDir = playerPosition - enemyPosition;
        moveDir = moveDir.normalized;

        bool canMove = !Physics.CapsuleCast(transform.position, PlayerGO.transform.position, GetComponent<CapsuleCollider>().radius, moveDir);
        if (!canMove)
        {
            //Randomize the side the Enemy will move to if it can't move towards player on the X axis
            Vector3 moveDirX = randomSide ? new Vector3(moveDir.x, 0f, 0f).normalized.Abs() : -new Vector3(moveDir.x, 0f, 0f).normalized.Abs();
            canMove = !Physics.CapsuleCast(transform.position, PlayerGO.transform.position, GetComponent<CapsuleCollider>().radius, moveDir);
            if (canMove)
            {
                moveDir = moveDirX;
            }

            else
            {
                //Randomize the side the enemy will move to if it can't move towards the player on the Z axis
                Vector3 moveDirZ = randomSide ? new Vector3(0f, 0f, moveDir.z).normalized.Abs() : -new Vector3(0f, 0f, moveDir.z).normalized.Abs();
                canMove = !Physics.CapsuleCast(transform.position, PlayerGO.transform.position, GetComponent<CapsuleCollider>().radius, moveDir);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else moveDir = Vector3.zero;
            }
        }

        isWalking = true;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        if (OnWalking != null) OnWalking(this, EventArgs.Empty); //fire Walking event

        HandleRotation();
    }

    private void HandleAttack()
    {
        isAttacking = true;
        isWalking = false;
        if (OnAttack != null) OnAttack(this, EventArgs.Empty); //fire Attack event
    }
    private void HandleIdle()
    {
        isWalking = false;
        isAttacking = false;
        if (OnIdle != null) OnIdle(this, EventArgs.Empty); //fire Idle event
    }



    private void HandleRotation()
    {
        float rotationSpeed = 10f;
        Vector3 playerDir = (playerPosition - enemyPosition).normalized;
        transform.forward = Vector3.Slerp(transform.forward, playerDir, Time.deltaTime * rotationSpeed);
    }

    public void TakeDamage(float damage)
    {
        if (canBeDamaged)
        {
            HealthChanged(-damage);
            StartCoroutine(CanBeDamagedCooldown(1f)); //todo: make this sword damage cooldown 
        }
    }

    public void Heal(float amount)
    {
        HealthChanged(amount);
    }

    public void HealthChanged(float amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
        if (OnHealthChanged != null) OnHealthChanged(this, amount);
        if (health <= 0) Die();
    }
    private void Die()
    {
        isDead = true;
        if (OnEnemyDeath != null) OnEnemyDeath(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
    public bool InAttackingRange()
    {
        return Vector3.Distance(playerPosition, enemyPosition) <= attackRange;
    }

    public void SetIsWalking(bool walking)
    {
        isWalking = walking;
    }

    public void SetIsAttacking(bool attacking)
    {
        isAttacking = attacking;
    }
    public float GetAttackRange()
    {
        return attackRange;
    }

    IEnumerator CanBeDamagedCooldown(float cooldown)
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(cooldown);
        canBeDamaged = true;
    }


}
