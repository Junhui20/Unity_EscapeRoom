using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CylinderButton : MonoBehaviour
{
    public Transform buttonTransform; // Assign the button (cylinder) transform
    public GameObject uvText; // Assign the UV text object
    public float pressDepth = 0.02f; // How far the button moves when pressed
    public float resetDelay = 0.3f; // Delay before resetting the button

    private Vector3 initialPosition;
    private bool isPressed = false;

    void Start()
    {
        initialPosition = buttonTransform.localPosition;
        uvText.SetActive(false); // Ensure UV text is hidden initially

        // Get XR Simple Interactable component
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnButtonPress);
    }

    public void OnButtonPress(SelectEnterEventArgs args)
    {
        if (isPressed) return; // Prevent multiple presses
        isPressed = true;

        // Move button down
        buttonTransform.localPosition = initialPosition + Vector3.down * pressDepth;

        // Show the UV text
        uvText.SetActive(true);

        // Reset button after delay
        Invoke(nameof(ResetButton), resetDelay);
    }

    private void ResetButton()
    {
        buttonTransform.localPosition = initialPosition;
        isPressed = false;
    }
}
