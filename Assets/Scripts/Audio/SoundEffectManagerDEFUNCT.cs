using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManagerDEFUNCT : MonoBehaviour
{
    public static SoundEffectManagerDEFUNCT instance;

    [SerializeField] private AudioSource soundEffectObject;

    private void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //unnessecary, will delete if no errors come up later
    public void PlaySoundEffectClipTestAudio(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn in the gameobject
        AudioSource audioSource = Instantiate(soundEffectObject, spawnTransform.position, Quaternion.identity);

        //assign the audioclip
        audioSource.clip = audioClip;

        //assign the volume
        audioSource.volume = volume;

        //play sound
        audioSource.Play();

        //get audioclip length
        float clipLength = audioSource.clip.length;

        //destroy the clip after it's finished
        Destroy(audioSource.gameObject, clipLength);
    }
}
