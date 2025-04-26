using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FloatingObject : MonoBehaviour
{
    public float floatHeight = 1.0f; // Height at which the object floats
    public float floatSpeed = 1.0f; // Speed of floating motion

    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;
    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Store the initial position of the object
        startPosition = transform.position;

        // Subscribe to grab events
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    void Update()
    {
        // If the object is not grabbed, make it float
        if (!grabInteractable.isSelected)
        {
            FloatInAir();
        }
    }

    void FloatInAir()
    {
        // Calculate the target position for floating
        Vector3 targetPosition = new Vector3(startPosition.x, floatHeight, startPosition.z);

        // Move the object towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, floatSpeed * Time.deltaTime);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Enable gravity and disable kinematic when grabbed
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // Disable gravity and enable kinematic when released
        rb.useGravity = false;
        rb.isKinematic = true;
    }
}