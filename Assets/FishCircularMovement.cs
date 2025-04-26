using UnityEngine;

public class FishCircularMovement : MonoBehaviour
{
    public float radius = 2f; // Radius of the circular path
    public float speed = 1f; // Speed of movement
    public Vector3 centerPosition; // Center of the circular path

    private float angle = 0f; // Current angle in radians

    void Start()
    {
        // Set the center position to the aquarium's center
        if (centerPosition == Vector3.zero)
        {
            centerPosition = transform.parent.position; // Assuming the aquarium is the parent
        }
    }

    void Update()
    {
        // Calculate the new position in a circular path
        float x = centerPosition.x + Mathf.Cos(angle) * radius;
        float z = centerPosition.z + Mathf.Sin(angle) * radius;

        // Update the fish's position
        transform.position = new Vector3(x, centerPosition.y, z);

        // Increment the angle based on speed and time
        angle += speed * Time.deltaTime;

        // Keep the angle within 0 to 2π to prevent overflow
        if (angle >= 2 * Mathf.PI)
        {
            angle -= 2 * Mathf.PI;
        }

        // Rotate the fish to face the direction of movement
        Vector3 direction = new Vector3(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
        transform.rotation = Quaternion.LookRotation(direction);
    }
}