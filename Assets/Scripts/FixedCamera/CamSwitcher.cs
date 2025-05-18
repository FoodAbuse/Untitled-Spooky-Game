using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSwitcher : MonoBehaviour
{
    public Transform Player;
    public CinemachineVirtualCamera activeCam;
    bool staticCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activeCam.Priority = 2;
        }
    }

    private void OnTriggerExit(Collider other)
    
    {
        if (other.CompareTag("Player"))
        {
            activeCam.Priority = 0;
        }
    }


}
