using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class LockPasswordSystem : MonoBehaviour
{
    [Header("Password Display")]
    public TextMeshPro passDisplay;

    [Header("Buttons")]
    public XRSimpleInteractable[] upButtons;
    public XRSimpleInteractable[] downButtons;

    [Header("Password Settings")]
    public GameObject C08Object;
    public float buttonPressDepth = 0.005f;
    public float buttonPressDuration = 0.2f;

    private char[] currentChars = new char[3] { 'A', '0', '0' };
    private Vector3[] upButtonOriginalPositions;
    private Vector3[] downButtonOriginalPositions;
    private Coroutine[] buttonAnimations;

    void Start()
    {
        buttonAnimations = new Coroutine[upButtons.Length + downButtons.Length];

        // Store original button positions
        upButtonOriginalPositions = new Vector3[upButtons.Length];
        downButtonOriginalPositions = new Vector3[downButtons.Length];

        for (int i = 0; i < upButtons.Length; i++)
        {
            if (upButtons[i] != null)
                upButtonOriginalPositions[i] = upButtons[i].transform.localPosition;
        }
        for (int i = 0; i < downButtons.Length; i++)
        {
            if (downButtons[i] != null)
                downButtonOriginalPositions[i] = downButtons[i].transform.localPosition;
        }

        // Setup button listeners
        for (int i = 0; i < 3; i++)
        {
            int index = i;
            if (upButtons[i] != null)
            {
                upButtons[i].selectEntered.AddListener(delegate { OnButtonPressed(index, 1); });
                upButtons[i].selectExited.AddListener(delegate { OnButtonReleased(index, true); });
            }
            if (downButtons[i] != null)
            {
                downButtons[i].selectEntered.AddListener(delegate { OnButtonPressed(index, -1); });
                downButtons[i].selectExited.AddListener(delegate { OnButtonReleased(index, false); });
            }
        }

        UpdateDisplay();
    }

    private void OnButtonPressed(int index, int change)
    {
        // Stop any existing animation
        if (buttonAnimations[index] != null)
        {
            StopCoroutine(buttonAnimations[index]);
        }

        // Start press animation
        buttonAnimations[index] = StartCoroutine(AnimateButtonPress(index, change > 0));

        ChangeCharacter(index, change);
    }

    private void OnButtonReleased(int index, bool isUpButton)
    {
        // Stop any existing animation
        if (buttonAnimations[index] != null)
        {
            StopCoroutine(buttonAnimations[index]);
        }

        // Start release animation
        buttonAnimations[index] = StartCoroutine(AnimateButtonRelease(index, isUpButton));
    }

    private System.Collections.IEnumerator AnimateButtonPress(int index, bool isUpButton)
    {
        Transform buttonTransform = isUpButton ? upButtons[index].transform : downButtons[index].transform;
        Vector3 targetPosition = isUpButton ?
            upButtonOriginalPositions[index] + Vector3.down * buttonPressDepth :
            downButtonOriginalPositions[index] + Vector3.down * buttonPressDepth;

        float elapsed = 0f;
        Vector3 startPos = buttonTransform.localPosition;

        while (elapsed < buttonPressDuration * 0.3f)
        {
            buttonTransform.localPosition = Vector3.Lerp(startPos, targetPosition, elapsed / (buttonPressDuration * 0.3f));
            elapsed += Time.deltaTime;
            yield return null;
        }

        buttonTransform.localPosition = targetPosition;
    }

    private System.Collections.IEnumerator AnimateButtonRelease(int index, bool isUpButton)
    {
        Transform buttonTransform = isUpButton ? upButtons[index].transform : downButtons[index].transform;
        Vector3 targetPosition = isUpButton ? upButtonOriginalPositions[index] : downButtonOriginalPositions[index];

        float elapsed = 0f;
        Vector3 startPos = buttonTransform.localPosition;

        while (elapsed < buttonPressDuration * 0.7f)
        {
            float t = elapsed / (buttonPressDuration * 0.7f);
            // Elastic easing approximation
            t = Mathf.Sin(t * Mathf.PI * 0.5f);
            buttonTransform.localPosition = Vector3.Lerp(startPos, targetPosition, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        buttonTransform.localPosition = targetPosition;
    }

    private void ChangeCharacter(int index, int change)
    {
        if (index == 0) // Letter
        {
            int charCode = currentChars[0];
            charCode += change;
            if (charCode > 'Z') charCode = 'A';
            if (charCode < 'A') charCode = 'Z';
            currentChars[0] = (char)charCode;
        }
        else // Digit
        {
            int digit = currentChars[index] - '0';
            digit += change;
            if (digit > 9) digit = 0;
            if (digit < 0) digit = 9;
            currentChars[index] = (char)(digit + '0');
        }

        UpdateDisplay();
        CheckPassword();
    }

    private void UpdateDisplay()
    {
        if (passDisplay != null)
        {
            passDisplay.text = string.Format("{0}{1}{2}", currentChars[0], currentChars[1], currentChars[2]);
        }
    }

    private void CheckPassword()
    {
        bool isCorrect = currentChars[0] == 'C' && currentChars[1] == '0' && currentChars[2] == '8';
        if (C08Object != null)
        {
            C08Object.SetActive(!isCorrect);
        }
    }
}