using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrabInput : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private GameObject disableInteractionScreen;
    [SerializeField]
    private GameObject pictureWindow;
    [SerializeField]
    private AudioSource errorSound;
    [SerializeField]
    private AudioSource correctSound;

    private string inputString;

    void Start()
    {
        disableInteractionScreen.SetActive(false);
        pictureWindow.SetActive(false);
    }

    public void ReadInput(string s)
    {
        inputString = s;
        Debug.Log(inputString);

        if (inputString == "4441")
        {
            Debug.Log("Correct Input");
            correctSound.Play();
            disableInteractionScreen.SetActive(true);
            pictureWindow.SetActive(true);
            inputField.text = "";
            inputString = null;
        }
        else
        {
            inputField.text = "";
            inputString = null;
            errorSound.Play();
        }
    }
}
