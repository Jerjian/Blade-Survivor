using Cinemachine.Utility;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float moveSpeed = 7f;

    private bool isWalking = false;
    private Vector3 playerPosition;
    private Vector3 enemyPosition;
    private float attackRange = 2f;
    private bool isAttacking = false;
    private bool randomSide;


    private void Awake()
    {
        isWalking = false;
        randomSide = Random.Range(0, 2) == 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        playerPosition = player.transform.position;
        enemyPosition = transform.position;
        HandleMovement();
    }
    private void HandleMovement()
    {
        Vector3 moveDir = playerPosition - enemyPosition;
        moveDir = moveDir.normalized;

        bool canMove = !Physics.BoxCast(transform.position, Vector3.one * attackRange / 2, moveDir, Quaternion.identity, moveSpeed * Time.deltaTime);

        if (!canMove)
        {
            //Randomize the side the Enemy will move to if it can't move towards player on the X axis
            Vector3 moveDirX = randomSide ? new Vector3(moveDir.x, 0f, 0f).normalized.Abs() : -new Vector3(moveDir.x, 0f, 0f).normalized.Abs();
            canMove = !Physics.BoxCast(transform.position, Vector3.one * attackRange / 2, moveDirX, Quaternion.identity, moveSpeed * Time.deltaTime);
            if (canMove)
            {
                moveDir = moveDirX;
            }

            else
            {
                //Randomize the side the enemy will move to if it can't move towards the player on the Z axis
                Vector3 moveDirZ = randomSide ? new Vector3(0f, 0f, moveDir.z).normalized.Abs() : -new Vector3(0f, 0f, moveDir.z).normalized.Abs();
                canMove = !Physics.BoxCast(transform.position, Vector3.one * attackRange / 2, moveDirZ, Quaternion.identity, moveSpeed * Time.deltaTime);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else moveDir = Vector3.zero;
            }
        }


        float distanceToPlayer = Vector3.Distance(playerPosition, enemyPosition);
        if (distanceToPlayer <= attackRange)
        {
            isAttacking = true;
            isWalking = false;
        }
        else isAttacking = false;

        if (canMove && !isAttacking)
        {
            isWalking = true;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        else isWalking = false;

        //Rotate enemy
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
}
