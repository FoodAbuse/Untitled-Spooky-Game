using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusicFadeOut : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicSource;
    
    private bool isFading;
    private float musicVolume;
    private float fadeSpeed;
    private float fadeAcceleration;

    void Start()
    {
        musicVolume = 0.6f;
        fadeSpeed = 1.2f;
        fadeAcceleration = 3f;   

        if (musicSource != null)
        {
            isFading = false;
            musicSource.volume = musicVolume;
        }
        else
        {
            isFading = true;
        }
    }

    void Update()
    {
        if (isFading && musicSource.volume > 0)
        {
            musicSource.volume = Mathf.Lerp(musicSource.volume, 0, fadeAcceleration * Time.deltaTime);
        }
    }

    public void FadeOutMusic()
    {
        if (!isFading)
        {
            isFading = true;
        }
    }
}
