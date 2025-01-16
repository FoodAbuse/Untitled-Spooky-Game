using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, soundEffectSounds;
    public AudioSource musicSource, soundEffectSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //later we can create a seperate function that will be able to randomise music and play certain containers depending on the scene.
        PlayMusic("BedroomTrack1", true);


    }

    //loop bool will be removed in future
    public void PlayMusic(string name, bool loop = false)
    {
        Sound s = Array.Find(musicSounds, x => x.audioName == name);

        if (s == null)
        {
            Debug.Log("Erm guys we got a problem, the sound can't be found");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
    }

    public void PlaySoundEffect (string name, float volume = 1f, float pitch = 1f)
    {
        Sound s = Array.Find(soundEffectSounds, x => x.audioName == name);

        if (s == null)
        {
            Debug.Log("Erm guys we got a problem, the sound can't be found");
        }

        else
        {
            soundEffectSource.pitch = pitch;
            soundEffectSource.PlayOneShot(s.clip, Mathf.Clamp01(volume));
        }
    }

    //these functions can be called once we have the ui set up for the audio menu
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSoundEffects()
    {
        soundEffectSource.mute = !soundEffectSource.mute;
    }

    //these can either be used later to control the ui volume sliders, or we can experiment with different audio outputs (probably the better option)
    public void MusicVolume (float volume)
    {
        musicSource.volume = volume;
    }

    public void SoundEffectVolume (float volume)
    {
        soundEffectSource.volume = volume;
    }
}
