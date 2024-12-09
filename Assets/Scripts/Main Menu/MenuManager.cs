using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    // Start is called before the first frame update
    void Start()
    {
        // Check for saves, set validSave true if found
        VerifyLoadableSave();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
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
