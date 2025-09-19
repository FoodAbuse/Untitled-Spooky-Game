using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InspectionSystem : MonoBehaviour
{
    [SerializeField]
    private Transform rotationTransform;

    [SerializeField]
    private GameObject itemToInspect;
    [SerializeField]
    private Vector3 itemScale;

    private Quaternion rotationOffset;

    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private float resetSpeed = 100f;
    [SerializeField]
    private float resetDelay = 5f;
    [SerializeField]
    private float resetTimer = 0f;

    [SerializeField]
    private GameObject canvasImage;
    [SerializeField]
    private GameObject canvasText;

    [SerializeField]
    private Vector3 previousMousePos;

    [SerializeField]
    private Quaternion defaultRotation;

    public bool itemReleased = false;

    private void Awake()
    {
        if(rotationTransform != null)
        {
            defaultRotation = rotationTransform.rotation;
            rotationOffset = rotationTransform.rotation;

            rotationTransform.rotation = new Quaternion(0,0,0,0);
            if(itemToInspect!=null)
            {
                Instantiate(itemToInspect, rotationTransform);
                rotationTransform.rotation = rotationOffset;
                
                GameObject spawnedItem = rotationTransform.GetChild(0).gameObject;
                
                rotationTransform.GetComponentInChildren<Transform>().localScale=itemScale;
                int layerInspection = LayerMask.NameToLayer("Inspection");
                spawnedItem.layer = layerInspection;
                foreach(Transform child in spawnedItem.transform.GetComponentInChildren<Transform>())
                {
                    child.gameObject.layer = layerInspection;
                }
                canvasImage.SetActive(true);
                canvasText.GetComponent<TMPro.TextMeshProUGUI>().text = itemToInspect.name;
            }
        }
        else
        {
            defaultRotation.x = 0;
            defaultRotation.y = 0;
            defaultRotation.z = 0;
        }

       
    }


    void Update()
    {
        if (rotationTransform != null)
        {
            

            if (Input.GetMouseButtonDown(0))
            {
                itemReleased = false;
                resetTimer = 0f;
                previousMousePos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 deltaMousePos = Input.mousePosition - previousMousePos;
                float rotX = deltaMousePos.y * rotationSpeed * Time.deltaTime;
                float rotY = -deltaMousePos.x * rotationSpeed * Time.deltaTime;

                Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
                rotationTransform.rotation = rot * rotationTransform.rotation;

                previousMousePos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                itemReleased = true;
            }

            if (itemReleased)
            {
                resetTimer += Time.deltaTime;
                if(resetTimer >= resetDelay)
                {
                    if(rotationTransform.rotation == defaultRotation)
                    {
                        itemReleased = false;
                    }
                    else
                    {
                        SetBlendedRotation();
                    }
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            canvasImage.SetActive(false);
            gameObject.SetActive(false);            
        }
    }

    public void SetBlendedRotation()
    {
        rotationTransform.rotation = Quaternion.RotateTowards(rotationTransform.rotation, defaultRotation, resetSpeed * Time.deltaTime);
    }
}
