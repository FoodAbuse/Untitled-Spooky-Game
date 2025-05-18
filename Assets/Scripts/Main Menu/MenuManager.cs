using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] toBeHidden;

    [SerializeField]
    private Animator titleAnimator;

    [SerializeField]
    private Animator inputAnimator;

    [SerializeField]
    private bool validSave = false;
    [SerializeField]
    private Button loadSaveButton;

    //PlayerControls playerControls;

    [SerializeField]
    private bool isAnyKeyPressed = false;

    // Start is called before the first frame update
    void Awake()
    {
        // Check for saves, set validSave true if found
        VerifyLoadableSave();
        //playerControls = new PlayerControls();

    }

    private void OnEnable() 
    {
        //playerControls.Enable();
    }
    private void OnDisable() 
    {
        //playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isAnyKeyPressed)
        {
            //isAnyKeyPressed = playerControls.PlayerMovement.AnyKey.ReadValue<float>() > 0.1f;
        } 

        if(isAnyKeyPressed)
        {
            ProgressToTitle();
        }
    }

    void ProgressToTitle()
    {
        /*
        foreach(GameObject g in toBeHidden)
        {
            g.SetActive(false);
        }
        */

        inputAnimator.SetTrigger("StartTransition");
    }

    public void VerifyLoadableSave()
    {
        if(loadSaveButton!=null)
        {
            if (validSave)
            {

                loadSaveButton.interactable = true;
            }
            else
            {
                loadSaveButton.interactable = false;
            }
        }
        
    }

    public void NewGameButtonPressed()
    {
        if(!validSave)
        {
            //load game intro scene
            Animator canvasAnim = transform.GetChild(0).gameObject.GetComponent<Animator>();

            if(canvasAnim!=null)
            {
                canvasAnim.SetTrigger("FadeOut");
            }
        }

        else
        {
            //warn player about losing autosave data
        }
    }


    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
