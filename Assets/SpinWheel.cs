using UnityEngine;

public class SpinWheel : MonoBehaviour
{
    public float spinSpeed = 100f; // Speed of rotation in degrees per second
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Freeze rotation on X and Z axes
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        // Apply torque to spin the wheel around its Y-axis
        rb.AddTorque(transform.up * spinSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}