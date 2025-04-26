using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class TreasureChest : MonoBehaviour
{
    [Header("Chest Components")]
    public GameObject lockObject;
    public GameObject treasureAbove;
    public float openAngle = -90f;
    public float openSpeed = 2f;
    public float moveDistance = 0.2f;

    [Header("Password System")]
    public TextMeshPro letterDisplay;
    public XRSimpleInteractable[] upButtons;
    public XRSimpleInteractable[] downButtons;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private char[] currentLetters = new char[4] { 'A', 'B', 'C', 'D' }; // Initial letters
    private readonly char[] correctPassword = new char[4] { 'J', 'U', 'L', 'Y' };

    void Start()
    {
        // Setup chest animations
        closedRotation = treasureAbove.transform.rotation;
        closedPosition = treasureAbove.transform.position;
        openRotation = Quaternion.Euler(closedRotation.eulerAngles.x + openAngle,
                                      closedRotation.eulerAngles.y,
                                      closedRotation.eulerAngles.z);
        openPosition = closedPosition + new Vector3(-moveDistance, 0, 0);

        // Setup button listeners
        for (int i = 0; i < 4; i++)
        {
            int index = i; // Important for closure
            upButtons[i].selectEntered.AddListener(_ => ChangeLetter(index, 1));
            downButtons[i].selectEntered.AddListener(_ => ChangeLetter(index, -1));
        }

        // Initialize display
        UpdateDisplay();
    }

    void Update()
    {
        // Smoothly animate the lid
        treasureAbove.transform.rotation = Quaternion.Slerp(
            treasureAbove.transform.rotation,
            isOpen ? openRotation : closedRotation,
            Time.deltaTime * openSpeed
        );

        treasureAbove.transform.position = Vector3.Lerp(
            treasureAbove.transform.position,
            isOpen ? openPosition : closedPosition,
            Time.deltaTime * openSpeed
        );
    }

    private void ChangeLetter(int index, int change)
    {
        // Get current letter's ASCII code
        int charCode = currentLetters[index];
        charCode += change;

        // Wrap around from Z to A and vice versa
        if (charCode > 'Z') charCode = 'A';
        if (charCode < 'A') charCode = 'Z';

        currentLetters[index] = (char)charCode;
        UpdateDisplay();
        CheckPassword();
    }

    private void UpdateDisplay()
    {
        letterDisplay.text = $"{currentLetters[0]}{currentLetters[1]}{currentLetters[2]}{currentLetters[3]}";
    }

    private void CheckPassword()
    {
        bool correct = true;
        for (int i = 0; i < 4; i++)
        {
            if (currentLetters[i] != correctPassword[i])
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            // Password is correct - unlock the chest
            isOpen = true;
            lockObject.SetActive(false);
        }
        else if (isOpen)
        {
            // Re-lock if incorrect after being open
            isOpen = false;
            lockObject.SetActive(true);
        }
    }
}