using UnityEngine;

public class ButtonPressAnimation : MonoBehaviour
{
    public Vector3 pressedPositionOffset = new Vector3(0, -0.01f, 0); // How far the button moves when pressed
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void OnButtonPressed()
    {
        transform.localPosition = originalPosition + pressedPositionOffset;
    }

    public void OnButtonReleased()
    {
        transform.localPosition = originalPosition;
    }
}