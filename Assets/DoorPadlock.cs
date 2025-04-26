using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Attach this script to your Padlock UI panel
public class DoorPadlock : MonoBehaviour
{
    [Header("Padlock Settings")]
    [Tooltip("4-digit code required to unlock the door")]
    public string correctCode = "1234";
    private string inputCode = "";

    [Header("UI References")]
    public TextMeshProUGUI displayText;
    public Button[] numberButtons;
    public Button clearButton;
    public Button enterButton;

    [Header("Feedback Settings")]
    public float feedbackDisplayTime = 1.5f;
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public Color defaultColor = Color.white;

    [Header("Door References")]
    public Animator doorAnimator;

    private bool showActualNumbers = true;
    private float feedbackTimer = 0f;
    private bool showingFeedback = false;
    private string feedbackMessage = "";

    void Start()
    {
        // Hook up number buttons
        for (int i = 0; i < numberButtons.Length; i++)
        {
            string digit = i.ToString();
            numberButtons[i].onClick.AddListener(() => AppendDigit(digit));
        }

        clearButton.onClick.AddListener(ClearInput);
        enterButton.onClick.AddListener(SubmitCode);

        // Initialize display
        UpdateDisplay();
    }

    void Update()
    {
        // Handle feedback timer
        if (showingFeedback)
        {
            feedbackTimer -= Time.deltaTime;

            if (feedbackTimer <= 0)
            {
                showingFeedback = false;
                ClearInput();
                displayText.color = defaultColor;
                UpdateDisplay();
            }
        }
    }

    // Add a digit when a number button is pressed
    void AppendDigit(string digit)
    {
        if (showingFeedback)
            return;

        if (inputCode.Length >= 4)
            return; // Prevent more than 4 digits

        inputCode += digit;
        UpdateDisplay();
    }

    // Clear the current input
    void ClearInput()
    {
        inputCode = string.Empty;
        UpdateDisplay();
    }

    // Check if the entered code matches
    void SubmitCode()
    {
        if (showingFeedback)
            return;

        if (inputCode.Length < 4)
        {
            ShowFeedback("Enter 4 digits", incorrectColor);
            return;
        }

        if (inputCode == correctCode)
        {
            // Unlock door
            // Enable the Animator first
            doorAnimator.enabled = true;

            // Then trigger the animation
            doorAnimator.SetTrigger("Open");


            ShowFeedback("CORRECT", correctColor);
            UnityEngine.Debug.Log("Door Unlocked!");
        }
        else
        {
            ShowFeedback("INCORRECT", incorrectColor);
            UnityEngine.Debug.Log("Incorrect Code. Try again.");
        }
    }

    // Show feedback to the user
    void ShowFeedback(string message, Color color)
    {
        showingFeedback = true;
        feedbackTimer = feedbackDisplayTime;
        feedbackMessage = message;

        if (displayText != null)
        {
            displayText.text = message;
            displayText.color = color;
        }
    }

    // Update the on-screen display
    void UpdateDisplay()
    {
        if (displayText == null)
        {
            UnityEngine.Debug.LogError("DisplayText reference is null!");
            return;
        }

        if (showingFeedback)
        {
            // Already showing feedback message
            return;
        }

        string displayString = "";

        if (showActualNumbers)
        {
            // Show actual numbers
            displayString = inputCode.PadRight(4, '_');
        }
        else
        {
            // Show asterisks for entered digits
            displayString = new string('*', inputCode.Length).PadRight(4, '_');
        }

        displayText.text = displayString;
        UnityEngine.Debug.Log("Display updated: " + displayString);
    }

    // Toggle between showing actual numbers or asterisks
    public void ToggleNumberDisplay()
    {
        showActualNumbers = !showActualNumbers;
        UpdateDisplay();
    }
}
