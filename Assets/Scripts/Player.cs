using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    public static Player Instance { get; private set; } //Singleton
    private bool isWalking;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject); //there is already a player instance
        else Instance = this;
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


}
