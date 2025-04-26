using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Numberpadlock : MonoBehaviour
{
    public string correctCode = "1234";
    private string input = "";

    public TextMeshProUGUI inputDisplay;
    public GameObject door;

    public void PressNumber(string number)
    {
        if (input.Length < correctCode.Length)
        {
            input += number;
            UpdateDisplay();
        }
    }

    public void ClearInput()
    {
        input = "";
        UpdateDisplay();
    }

    public void SubmitCode()
    {
        if (input == correctCode)
        {
            UnlockDoor();
        }
        else
        {
            input = "";
            UpdateDisplay();
        }
    }

    void UpdateDisplay()
    {
        inputDisplay.text = input;
    }

    void UnlockDoor()
    {
        // Replace with your door animation or opening logic
        door.SetActive(false);
    }
}
