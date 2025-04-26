using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerController : MonoBehaviour
{
    [SerializeField] private Transform drawerTransform;
    [SerializeField] private Vector3 closedPosition;
    [SerializeField] private Vector3 openPosition;
    [SerializeField] private XRGrabInteractable handleInteractable;
    [SerializeField] private float drawerLimit = 0.3f; // Maximum distance drawer can open

    private Rigidbody drawerRigidbody;
    private Vector3 initialPosition;
    private bool isGrabbed = false;

    void Awake()
    {
        drawerRigidbody = GetComponent<Rigidbody>();
        initialPosition = drawerTransform.localPosition;

        // Force drawer to closed position on start
        drawerTransform.localPosition = closedPosition;

        // Setup handle interaction events
        if (handleInteractable != null)
        {
            handleInteractable.selectEntered.AddListener(OnHandleGrabbed);
            handleInteractable.selectExited.AddListener(OnHandleReleased);
        }
    }

    private void OnHandleGrabbed(SelectEnterEventArgs args)
    {
        isGrabbed = true;
    }

    private void OnHandleReleased(SelectExitEventArgs args)
    {
        isGrabbed = false;
    }

    void FixedUpdate()
    {
        if (!isGrabbed)
        {
            // Optional: Add slight resistance to keep drawer in place when not grabbed
            drawerRigidbody.velocity *= 0.9f;
        }

        // Constrain drawer movement
        Vector3 currentPos = drawerTransform.localPosition;
        currentPos.x = Mathf.Clamp(currentPos.x, closedPosition.x, closedPosition.x + drawerLimit);
        currentPos.y = closedPosition.y; // Lock Y position
        currentPos.z = closedPosition.z; // Lock Z position

        drawerTransform.localPosition = currentPos;
    }
}
