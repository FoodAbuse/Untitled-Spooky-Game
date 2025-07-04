using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventCaller : MonoBehaviour
{
    public AudioSource[] footstepAudios;
    private int firstNum;
    private int secondNum;
    
    void Start()
    {
        firstNum = Random.Range(0, footstepAudios.Length);
        secondNum = Random.Range(0, footstepAudios.Length);
    }

    // Update is called once per frame
    public void FootstepTrigger()
    {
        while (firstNum == secondNum)
        {
           firstNum = Random.Range(0, footstepAudios.Length);
        }



        footstepAudios[firstNum].pitch = Random.Range(0.9f, 1.1f);
        footstepAudios[firstNum].Play();
        secondNum = firstNum;

    }
}
