using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource meowAudio;

    public void PlayMeowAudio()
    {
        if (!meowAudio.isPlaying)
        {
            meowAudio.Play();
        }
    }
}
