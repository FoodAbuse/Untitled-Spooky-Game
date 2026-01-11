using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTripwire : MonoBehaviour
{
    [SerializeField]
    private GameObject boundCamera;

    [SerializeField]
    private GameObject toEnable;
    [SerializeField]
    private GameObject toDisable;
    [SerializeField]
    private GameObject cameraMovementVector;

    void Start()
    {
        if(boundCamera==null)
        {
            Debug.Log("!!WARNING!! - No Camera Set for "+gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player")) //if the player is the collider that has set the trigger off
        {
            if (toEnable != null) // if there are objects in our list that we need to enable
            {
                toEnable.SetActive(true);
                Debug.Log("enabling " + toEnable.name);
            }
            else
                Debug.Log("!!WARNING!! - No object in enable list for " + gameObject.name);
            if (toDisable != null)
            {
                toDisable.SetActive(false);
                Debug.Log("disabling " + toDisable.name);
            }
            GameObject oldCamera = Camera.main.gameObject;
            if(boundCamera!=null)
            {
                if (oldCamera!=boundCamera)
                {
                    boundCamera.SetActive(true);
                    oldCamera.tag = "Untagged";
                    boundCamera.tag = "MainCamera";
                    oldCamera.SetActive(false);
                }       

            }

            if(cameraMovementVector!=null)
            {
                other.GetComponent<HugoMovementTests>().PassCamOrentation(cameraMovementVector.transform);
            }
            else
            {
                Debug.Log("!!WARNING!! - No assigned vector for transfer for camera '" + boundCamera.name + "'. using cameras native vector");
                other.GetComponent<HugoMovementTests>().PassCamOrentation(boundCamera.transform);
            }
            
            
            gameObject.SetActive(false);
        }
    }
}
