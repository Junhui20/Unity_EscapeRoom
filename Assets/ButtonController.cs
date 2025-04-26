using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonController : MonoBehaviour
{
    public PasswordDisplay passwordDisplay; // Reference to the PasswordDisplay script
    public int digitIndex; // Which digit this button controls (0, 1, or 2)
    public int incrementAmount; // +1 for up, -1 for down
    public XRBaseController vrController; // Reference to the VR controller

    private XRSimpleInteractable simpleInteractable;

    void Start()
    {
        simpleInteractable = GetComponent<XRSimpleInteractable>();
        simpleInteractable.selectEntered.AddListener(OnButtonPressed);
        simpleInteractable.selectExited.AddListener(OnButtonReleased);
        simpleInteractable.hoverEntered.AddListener(OnHoverEntered);
        simpleInteractable.hoverExited.AddListener(OnHoverExited);
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        passwordDisplay.ChangeDigit(digitIndex, incrementAmount);
        GetComponent<ButtonPressAnimation>().OnButtonPressed();
        vrController.SendHapticImpulse(0.5f, 0.1f); // Haptic feedback
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        passwordDisplay.ChangeDigit(digitIndex, incrementAmount);
        GetComponent<ButtonPressAnimation>().OnButtonPressed();
        vrController.SendHapticImpulse(0.5f, 0.1f); // Haptic feedback
    }

    private void OnButtonReleased(SelectExitEventArgs args)
    {
        GetComponent<ButtonPressAnimation>().OnButtonReleased();
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        GetComponent<ButtonPressAnimation>().OnButtonReleased();
    }
}