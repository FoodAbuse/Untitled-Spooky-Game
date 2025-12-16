using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    private float sfxLength;
    [SerializeField]
    private AudioSource comaAudio;
    [SerializeField]
    private AudioSource enterAudio;

    void Start()
    {
        sfxLength = 2.5f;
    }

    public void StartGameButton()
    {
        Debug.Log("Hiiiii!!! :3333");
        enterAudio.Play();
        StartCoroutine(StartGameSequence());
    }

    public void SettingsButton()
    {
        Debug.Log("Gruh");
        enterAudio.Play();
    }

    public void SilliCatsButton()
    {
        Debug.Log("HiiiiiiiIIii!!!! :3");
        enterAudio.Play();
    }

    public void QuitButton()
    {
        Debug.Log("Application has quit :((");
        enterAudio.Play();
        Application.Quit();
    }
    
    public IEnumerator StartGameSequence()
    {
        if (comaAudio != null)
        {
            sfxLength = comaAudio.clip.length;
            comaAudio.Play();
        }
        yield return new WaitForSeconds(sfxLength);
        yield return null;
        SceneManager.LoadScene("Toska Bedroom 2");
    }
}
