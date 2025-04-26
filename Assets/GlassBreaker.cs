using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GlassBreaker : MonoBehaviour
{
    public GameObject hammerObject; // Assign the hammer GameObject in the Inspector
    public AudioClip glassBreakSound; // Assign the glass break sound in the Inspector
    public float destroyDelay = 0.5f; // Delay before destroying to ensure sound plays

    private AudioSource audioSource;

    private void Start()
    {
        // Add an AudioSource component to the glass cube
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = glassBreakSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the hammer
        if (other.gameObject == hammerObject)
        {
            // Check if the hammer is being held (optional, if using XR Interaction Toolkit)
            XRGrabInteractable grabInteractable = hammerObject.GetComponent<XRGrabInteractable>();
            if (grabInteractable == null || grabInteractable.isSelected)
            {
                BreakGlass();
            }
        }
    }

    private void BreakGlass()
    {
        // Play the glass break sound
        if (glassBreakSound != null && audioSource != null)
        {
            audioSource.Play();
        }

        // Disable the mesh renderer and collider immediately so it appears to break
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Destroy the glass cube after a short delay to ensure sound plays
        Destroy(gameObject, destroyDelay);
    }
}