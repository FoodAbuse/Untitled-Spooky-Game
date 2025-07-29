using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerZone : MonoBehaviour
{
    [SerializeField]
    private SequencedCameraComponent boundCamera;

    [SerializeField]
    private enum triggeredCameraType {Default, StaticZone};

    [SerializeField]
    private triggeredCameraType zoneTriggerType = triggeredCameraType.Default;

    [SerializeField]
    private GameObject previousCamera;

    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        if(boundCamera == null)
        {
            Debug.LogError("Warning - No Camera Set to a Trigger Zone");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag==("Player"))
        {            
            if(zoneTriggerType==triggeredCameraType.StaticZone)
            {
                previousCamera = Camera.main.gameObject;
            }
            boundCamera.gameObject.SetActive(true);
            boundCamera.SetMainCamera();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(zoneTriggerType == triggeredCameraType.StaticZone)
        {
            boundCamera.gameObject.SetActive(false);
            previousCamera.SetActive(true);
            boundCamera.gameObject.tag="Untagged";
            previousCamera.tag = "MainCamera";
            
        }
    }




}
