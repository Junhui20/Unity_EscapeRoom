using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CatMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of movement
    public float turnSpeed = 100f; // Speed of turning
    public float minMoveTime = 1f; // Minimum time to move in one direction
    public float maxMoveTime = 3f; // Maximum time to move in one direction

    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;
    private Vector3 moveDirection;
    private float moveTime;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Initialize random movement
        SetRandomMovement();
    }

    void Update()
    {
        // If the cat is not grabbed, move randomly
        if (!grabInteractable.isSelected)
        {
            MoveRandomly();
        }
        else
        {
            // Stop movement when grabbed
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void MoveRandomly()
    {
        // Move in the current direction
        rb.velocity = moveDirection * moveSpeed;

        // Rotate towards the movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // Update the timer
        timer += Time.deltaTime;

        // Change direction after the move time has elapsed
        if (timer >= moveTime)
        {
            SetRandomMovement();
        }
    }

    void SetRandomMovement()
    {
        // Generate a random direction
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

        // Set a random move time
        moveTime = Random.Range(minMoveTime, maxMoveTime);

        // Reset the timer
        timer = 0f;
    }
}