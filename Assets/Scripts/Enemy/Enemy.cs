using Cinemachine.Utility;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    private GameObject PlayerGO;
    [SerializeField] private float moveSpeed = 7f;

    private bool isWalking = false;
    private Vector3 playerPosition;
    private Vector3 enemyPosition;
    private float attackRange = 2f;
    private bool isAttacking = false;
    private bool randomSide;

    public event EventHandler OnAttack;
    public event EventHandler OnWalking;
    public event EventHandler OnIdle;
    public event EventHandler<float> OnHealthChanged;

    private void Awake()
    {
        isWalking = false;
        randomSide = UnityEngine.Random.Range(0, 2) == 0;
        PlayerGO = GameObject.FindGameObjectWithTag("Player");
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

}
