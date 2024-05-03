using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    public static Player Instance { get; private set; } //Singleton
    private bool isWalking;
    private float health;
    private float maxHealth = 100f;
    private bool isDead;

    public event EventHandler OnPlayerDeath;
    public event EventHandler<float> OnHealthChanged; //Incase I need it later (add red border or green border)

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject); //there is already a player instance
        else Instance = this;
    }
    private void Start()
    {
        health = maxHealth;
        isDead = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // Get movement direction
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Collision detection
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // Check if we can move on the x axis
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) moveDir = moveDirX; //Can move only on the X axis
            else
            {
                // Check if we can move on the z axis
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) moveDir = moveDirZ; //Can move only on the Z axis
            }
        }
        // Move the player
        if (canMove) transform.position += moveDir * moveSpeed * Time.deltaTime;

        //update the walking state
        isWalking = moveDir != Vector3.zero;

        //Rotate the player
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

    }
    private void HandleInteractions()
    {

    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public void TakeDamage(int damage)
    {
        HealthChanged(-damage);
    }

    public void Heal(int amount)
    {
        HealthChanged(amount);
    }

    public void HealthChanged(int amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
        if (OnHealthChanged != null) OnHealthChanged(this, amount);
        if (health <= 0) Die();
    }

    public float MaxHealth
    {
        get { return maxHealth; }
    }

    public float Health
    {
        get { return health; }
    }
    private void Die()
    {
        isDead = true;
        if (OnPlayerDeath != null) OnPlayerDeath(this, EventArgs.Empty);
        Destroy(gameObject);
    }

}
