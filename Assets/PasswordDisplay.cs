using UnityEngine;
using TMPro;

public class PasswordDisplay : MonoBehaviour
{
    public TextMeshPro displayText; // Reference to the display text
    public GameObject disposalCube; // Reference to the glass cube
    public GameObject ViewHiddenObject; // Reference to the sphere inside the cube
    public GameObject RealHiddenObject; // Reference to the sphere spawned after password is correct
    private int[] passwordDigits = new int[3] { 0, 0, 0 }; // Initial password
    private int[] correctPassword = new int[3] { 2, 0, 7 }; // Correct password (change this to your desired password)

    void Start()
    {
        UpdateDisplay();
        if (ViewHiddenObject != null)
        {
            ViewHiddenObject.SetActive(true); // Ensure the sphere inside the cube is visible
        }
        if (RealHiddenObject != null)
        {
            RealHiddenObject.SetActive(false); // Ensure the spawned sphere is hidden at the start
        }
    }

    // Update the display with the current password
    public void UpdateDisplay()
    {
        displayText.text = $"{passwordDigits[0]}{passwordDigits[1]}{passwordDigits[2]}";
    }

    // Change a specific digit
    public void ChangeDigit(int index, int amount)
    {
        passwordDigits[index] = (passwordDigits[index] + amount + 10) % 10; // Wrap around 0-9
        UpdateDisplay();
        CheckPassword(); // Check if the password is correct after changing a digit
    }

    // Check if the password is correct
    private void CheckPassword()
    {
        bool isCorrect = true;
        for (int i = 0; i < 3; i++)
        {
            if (passwordDigits[i] != correctPassword[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            // Password is correct, make the cube and ViewHiddenObject disappear, and spawn RealHiddenObject
            if (disposalCube != null)
            {
                disposalCube.SetActive(false); // Disable the glass cube
            }
            if (ViewHiddenObject != null)
            {
                ViewHiddenObject.SetActive(false); // Disable the sphere inside the cube
            }
            if (RealHiddenObject != null)
            {
                RealHiddenObject.SetActive(true); // Enable the spawned sphere
            }
        }
    }
}
