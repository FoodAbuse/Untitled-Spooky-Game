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
    [SerializeField]
    private GameObject defaultCamera;

    public bool overrideExitCondition;
    public bool isTripwire;
    [SerializeField]
    private GameObject[] objectsToEnable;
    [SerializeField]
    private GameObject[] objectsToDisable;
    



    void Start()
    {
        overrideExitCondition = false;
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
            /*
            if (other.GetComponent<HugoMovementTests>().currentCamZone!=null)
            {
                //other.GetComponent<HugoMovementTests>().currentCamZone.GetComponent<CameraTriggerZone>().overrideExitCondition = true;
                other.GetComponent<HugoMovementTests>().PassCamZone(this);
                
            }
            else
                other.GetComponent<HugoMovementTests>().PassCamZone(this);
            */
            if (zoneTriggerType==triggeredCameraType.StaticZone)
            {
                previousCamera = Camera.main.gameObject;
                boundCamera.gameObject.SetActive(true);
                boundCamera.SetMainCamera();
            }
            if (zoneTriggerType==triggeredCameraType.Default)
            {
                defaultCamera.SetActive(true);
                boundCamera.gameObject.tag = "Untagged";
                defaultCamera.gameObject.tag = "MainCamera";
                boundCamera.gameObject.SetActive(false);

            }
            

            if (objectsToEnable!=null)
            {
                foreach (GameObject g in objectsToEnable)
                {
                    g.SetActive(true);
                }
            }
            
            if (objectsToDisable!=null)
            { 
                foreach(GameObject g in objectsToDisable)
                {
                    g.SetActive(false);
                }
            }
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(zoneTriggerType == triggeredCameraType.StaticZone && overrideExitCondition==false)
        {
            boundCamera.gameObject.SetActive(false);
            previousCamera.SetActive(true);
            boundCamera.gameObject.tag="Untagged";
            previousCamera.tag = "MainCamera";
            
        }
    }




}
