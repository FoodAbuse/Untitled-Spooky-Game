using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepTrigger : MonoBehaviour
{
    //assign variables
    public AudioSource[] footsteps;
    private int currentFootstep;
    private int prevFootstep;
    private float footstepVolume;


    void Start()
    {
        currentFootstep = Random.Range(0, footsteps.Length);
        prevFootstep = Random.Range(0, footsteps.Length);
        footstepVolume = 0.5f;
    }


    private void TriggerFootstepWalk()
    {
        //prevents audio from quietening if player is sprinting but just shifting direction
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            footstepVolume = 0.6f;
        }
        else
        {
            footstepVolume = 0.4f;
        }

        while (currentFootstep == prevFootstep)
        {
            currentFootstep = Random.Range(0, footsteps.Length);
        }

        if (!footsteps[prevFootstep].isPlaying)
        {
            footsteps[currentFootstep].pitch = Random.Range(0.88f, 1.08f);
            footsteps[currentFootstep].volume = footstepVolume;
            footsteps[currentFootstep].Play();
        }

        prevFootstep = currentFootstep;
    }

    private void TriggerFootstepSprint()
    {
        footstepVolume = 0.6f;

        while (currentFootstep == prevFootstep)
        {
            currentFootstep = Random.Range(0, footsteps.Length);
        }

        if (!footsteps[prevFootstep].isPlaying)
        {
            footsteps[currentFootstep].pitch = Random.Range(0.98f, 1.29f);
            footsteps[currentFootstep].volume = footstepVolume;
            footsteps[currentFootstep].Play();
        }

        prevFootstep = currentFootstep;
    }

}
