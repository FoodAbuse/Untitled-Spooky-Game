using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteFlashAudio : MonoBehaviour
{
    public AudioSource whiteFlashAudio;
    private bool canAudioPlay;
    void Start()
    {
        canAudioPlay = true;
    }

    void Update()
    {
        if(Input.anyKeyDown && canAudioPlay)
        {
            StartCoroutine(WhiteAudio());
        }
    }

    public IEnumerator WhiteAudio()
    {
        canAudioPlay = false;
        yield return new WaitForSeconds(1);
        whiteFlashAudio.Play();
    }
}
