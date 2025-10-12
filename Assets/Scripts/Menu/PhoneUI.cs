using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PhoneUI : MonoBehaviour
{
    [SerializeField]
    private GameObject volume;
    [SerializeField]
    private GameObject unpausedVolume;
    [SerializeField]
    private AudioSource openMenu;
    [SerializeField]
    private AudioSource openMenu2;
    [SerializeField]
    private AudioSource openMenu3;
    [SerializeField]
    private AudioSource openMenu4;
    [SerializeField]
    private AudioSource closeMenu;
    [SerializeField]
    private VideoPlayer glitchVideo1;
    [SerializeField]
    private GameObject glitchVideo1Object;
    [SerializeField]
    private VideoPlayer glitchVideo2;
    [SerializeField]
    private GameObject glitchVideo2Object;
    [SerializeField]
    private GameObject UIWindow;
    [SerializeField]
    private GameObject SettingsWindow;
    [SerializeField]
    private GameObject MessagesWindow;
    [SerializeField]
    private GameObject MeowsWindow;
    [SerializeField]
    private GameObject MenuWindow;
    [SerializeField]
    private GameObject TrashWindow;
    [SerializeField]
    private GameObject VaultWindow;
    [SerializeField]
    private GameObject PictureWindow;
    [SerializeField]
    private GameObject TrashText1Window;
    [SerializeField]
    private GameObject TrashText2Window;
    [SerializeField]
    private GameObject DisableTrashWindow;
    [SerializeField]
    private GameObject DisablePictureWindow;

    [SerializeField]
    private GameObject HiddenMessage1;
    [SerializeField]
    private GameObject HiddenMessage2;
    [SerializeField]
    private GameObject HiddenMessage3;
    [SerializeField]
    private GameObject HiddenMessage4;
    [SerializeField]
    private GameObject HiddenMess;
    [SerializeField]
    private GameObject DisableInteractionImage;
    [SerializeField]
    private GameObject musicPlayer;
    [SerializeField]
    private AudioSource pauseAmbience;

    private AudioLowPassFilter lowPassFilter;

    [SerializeField]
    private AudioLowPassFilter ambienceLowPassFilter;

    [SerializeField]
    private AudioSource hoverSFX;
    [SerializeField]
    private AudioSource clickSFX;

    private int randomAudio1;
    private int randomAudio2;

    private float videoLength1;
    private float videoLength2;

    private bool isPaused;
    private bool isUnpaused;
    private bool canUnpause;
    private bool unpauseExitButtonPressed;

    void Start()
    {
        randomAudio1 = 0;
        randomAudio2 = 2;

        UIWindow.SetActive(false);
        SettingsWindow.SetActive(false);
        MessagesWindow.SetActive(false);
        MeowsWindow.SetActive(false);
        MenuWindow.SetActive(false);
        TrashWindow.SetActive(false);
        VaultWindow.SetActive(false);
        PictureWindow.SetActive(false);
        TrashText1Window.SetActive(false);
        TrashText2Window.SetActive(false);
        DisableInteractionImage.SetActive(false);

        HiddenMessage1.SetActive(false);
        HiddenMessage2.SetActive(false);
        HiddenMessage3.SetActive(false);
        HiddenMessage4.SetActive(false);
        HiddenMess.SetActive(false);


        glitchVideo1Object.SetActive(false);
        glitchVideo1.frame = 0;
        glitchVideo1.Pause();

        glitchVideo2Object.SetActive(false);
        glitchVideo2.frame = 0;
        glitchVideo2.Pause();

        isPaused = false;
        isUnpaused = false;
        canUnpause = false;
        unpauseExitButtonPressed = false;

        volume.SetActive(false);

        if (volume != null)
        {
            Debug.Log("Volume detected");
        }

        if (musicPlayer != null)
        {
            lowPassFilter = musicPlayer.GetComponent<AudioLowPassFilter>();
        }

        if (lowPassFilter != null)
        {
            lowPassFilter.enabled = false;
        }

        ambienceLowPassFilter.enabled = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isPaused && !canUnpause)
        {
            StartCoroutine(OpenPauseMenu());
            pauseAmbience.Play();
        }
        else if ((Input.GetKeyDown(KeyCode.Tab) && isPaused && isUnpaused) || unpauseExitButtonPressed)
        {
            StartCoroutine(ClosePauseMenu());
            pauseAmbience.Stop();
        }
    }

    private IEnumerator OpenPauseMenu()
    {
        clickSFX.enabled = true;

        if (lowPassFilter != null)
        {
            lowPassFilter.enabled = true;
        }

        while (randomAudio1 == randomAudio2)
            {
                randomAudio1 = Random.Range(0, 4);
            }
        randomAudio2 = randomAudio1;

        switch (randomAudio1)
        {
            case 0:
                openMenu.Play();
                break;

            case 1:
                openMenu2.Play();
                break;

            case 2:
                openMenu3.Play();
                break;

            case 3:
                openMenu4.Play();
                break;
        }

        canUnpause = true;
        glitchVideo1.frame = 0;
        glitchVideo1Object.SetActive(true);

        //ensures that video won't freeze on any frames when playing
        if (!glitchVideo1.isPrepared)
        {
            glitchVideo1.Prepare();
            while (!glitchVideo1.isPrepared)
            {
                yield return null;
            }
        }
        //single frame delay to fix freezes (please work)
        yield return null;

        videoLength1 = (float)glitchVideo1.clip.length;

        glitchVideo1.Play();

        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(videoLength1);
        glitchVideo1.Stop();
        glitchVideo1.frame = 0;

        //this somehow stops the final frame from repeating again at the start of the video
        glitchVideo1.targetTexture?.Release();
        yield return null;
        glitchVideo1Object.SetActive(false);

        if (unpausedVolume != null)
        {
            unpausedVolume.SetActive(false);
        }

        volume.SetActive(true);
        UIWindow.SetActive(true);
        isPaused = true;
        isUnpaused = true;
    }

    private IEnumerator ClosePauseMenu()
    {
        if (lowPassFilter != null)
        {
            lowPassFilter.enabled = false;
        }

        //play sfx
        isUnpaused = false;
        unpauseExitButtonPressed = false;
        closeMenu.Play();

        UIWindow.SetActive(false);

        glitchVideo2Object.SetActive(true);
        glitchVideo2.frame = 0;

        //ensures that video won't freeze on any frames whilst playing
        if (!glitchVideo2.isPrepared)
        {
            glitchVideo2.Prepare();
            while (!glitchVideo2.isPrepared)
            {
                yield return null;
            }
        }
        //single frame delay
        yield return null;

        glitchVideo2.Play();

        //obtaining length of video
        videoLength2 = (float)glitchVideo2.clip.length;
        yield return new WaitForSecondsRealtime(videoLength2);

        glitchVideo2.frame = 0;
        glitchVideo2Object.SetActive(false);


        volume.SetActive(false);

        if (unpausedVolume != null)
        {
            unpausedVolume.SetActive(true);
        }

        isPaused = false;
        canUnpause = false;
        Time.timeScale = 1f;

        clickSFX.enabled = false;

        ExitMessages();
        ExitSettings();
        ExitMeowFolder();
        ExitMenu();
        ExitTrash();
        ExitTrashText1();
        ExitTrashText2();
        ExitVault();
        ExitPicture();

    }

    public void ButtonHover()
    {
        hoverSFX.Play();
    }

    public void UnpauseButton()
    {
        Debug.Log("Exit Button Pressed");
        unpauseExitButtonPressed = true;
    }

    public void MessagesButton()
    {
        Debug.Log("Messages Button Pressed");
        MessagesWindow.SetActive(true);
        DisableInteractionImage.SetActive(true);

        clickSFX.Play();

        HiddenMessage3.SetActive(true);
    }

    public virtual void ExitMessages()
    {
        Debug.Log("Messages Exited");
        MessagesWindow.SetActive(false);
        DisableInteractionImage.SetActive(false);

        HiddenMessage3.SetActive(false);

        clickSFX.Play();

        RectTransform exitMessagesRT = MessagesWindow.GetComponent<RectTransform>();

        exitMessagesRT.anchoredPosition = Vector2.zero;
    }

    public void MeowFolderButton()
    {
        Debug.Log("Meow Folder Button Pressed");
        MeowsWindow.SetActive(true);
        DisableInteractionImage.SetActive(true);

        pauseAmbience.volume = 0.3f;

        clickSFX.Play();

        ambienceLowPassFilter.enabled = true;
    }

    public void ExitMeowFolder()
    {
        Debug.Log("Meows Exited");
        MeowsWindow.SetActive(false);
        DisableInteractionImage.SetActive(false);

        ambienceLowPassFilter.enabled = false;

        clickSFX.Play();

        pauseAmbience.volume = 0.55f;

        RectTransform exitMeowsRT = MeowsWindow.GetComponent<RectTransform>();

        exitMeowsRT.anchoredPosition = Vector2.zero;
    }

    public void SettingsButton()
    {
        Debug.Log("Settings Button Pressed");
        SettingsWindow.SetActive(true);
        DisableInteractionImage.SetActive(true);

        HiddenMessage2.SetActive(true);

        clickSFX.Play();
    }

    public void ExitSettings()
    {
        Debug.Log("Settings Exited");
        SettingsWindow.SetActive(false);
        DisableInteractionImage.SetActive(false);

        clickSFX.Play();

        HiddenMessage2.SetActive(false);

        RectTransform exitSettingsRT = SettingsWindow.GetComponent<RectTransform>();

        exitSettingsRT.anchoredPosition = Vector2.zero;
    }

    public void MenuButton()
    {
        Debug.Log("Menu Button Pressed");
        MenuWindow.SetActive(true);
        DisableInteractionImage.SetActive(true);

        clickSFX.Play();

        HiddenMessage4.SetActive(true);
    }

    public void ExitMenu()
    {
        Debug.Log("Menu Exited");
        MenuWindow.SetActive(false);
        DisableInteractionImage.SetActive(false);

        clickSFX.Play();

        HiddenMessage4.SetActive(false);

        RectTransform exitMenuRT = MenuWindow.GetComponent<RectTransform>();

        exitMenuRT.anchoredPosition = Vector2.zero;
    }

    public void TrashButton()
    {
        Debug.Log("Trash Button Pressed");
        TrashWindow.SetActive(true);
        DisableInteractionImage.SetActive(true);
        HiddenMess.SetActive(true);
        HiddenMessage1.SetActive(true);

        clickSFX.Play();
    }

    public void ExitTrash()
    {
        Debug.Log("Trash Exited");
        TrashWindow.SetActive(false);
        DisableInteractionImage.SetActive(false);
        
        clickSFX.Play();

        DisableTrashWindow.SetActive(false);

        HiddenMess.SetActive(false);
        HiddenMessage1.SetActive(false);

        RectTransform exitTrashRT = TrashWindow.GetComponent<RectTransform>();

        exitTrashRT.anchoredPosition = Vector2.zero;
    }

    public void TrashText1Button()
    {
        Debug.Log("Trash Text 1 Pressed");
        TrashText1Window.SetActive(true);

        DisableTrashWindow.SetActive(true);

        clickSFX.Play();
    }

    public void ExitTrashText1()
    {
        Debug.Log("Trash Text 1 Exited");
        TrashText1Window.SetActive(false);

        DisableTrashWindow.SetActive(false);

        clickSFX.Play();

        RectTransform exitTrashText1RT = TrashText1Window.GetComponent<RectTransform>();

        exitTrashText1RT.anchoredPosition = new Vector2(68f, -108f);
    }
    public void TrashText2Button()
    {
        Debug.Log("Trash Text 2 Pressed");
        TrashText2Window.SetActive(true);
        DisableTrashWindow.SetActive(true);
        clickSFX.Play();
    }

    public void ExitTrashText2()
    {
        Debug.Log("Trash Text 2 Exited");
        TrashText2Window.SetActive(false);

        clickSFX.Play();
        
        DisableTrashWindow.SetActive(false);

        RectTransform exitTrashText2RT = TrashText2Window.GetComponent<RectTransform>();

        exitTrashText2RT.anchoredPosition = new Vector2(135f, -34f);
    }

    public void VaultButton()
    {
        Debug.Log("Vault Button Pressed");
        VaultWindow.SetActive(true);
        DisableInteractionImage.SetActive(true);

        clickSFX.Play();
    }

    public void ExitVault()
    {
        Debug.Log("Exited Vault");
        VaultWindow.SetActive(false);
        DisableInteractionImage.SetActive(false);
        
        clickSFX.Play();

        RectTransform exitVaultRT = VaultWindow.GetComponent<RectTransform>();

        exitVaultRT.anchoredPosition = Vector2.zero;
    }

    public void ExitPicture()
    {
        Debug.Log("Exited Picture");
        PictureWindow.SetActive(false);
        DisablePictureWindow.SetActive(false);
        
        clickSFX.Play();

        RectTransform exitPictureRT = PictureWindow.GetComponent<RectTransform>();

        exitPictureRT.anchoredPosition = new Vector2(68f, -108f);
    }

}
