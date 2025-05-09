using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionSystem : MonoBehaviour
{
    [SerializeField]
    private Transform objectToInspect;

    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private float resetSpeed = 100f;
    [SerializeField]
    private float resetDelay = 5f;
    [SerializeField]
    private float resetTimer = 0f;

    [SerializeField]
    private Vector3 previousMousePos;

    [SerializeField]
    private Quaternion defaultRotation;

    public bool itemReleased = false;

    private void Start()
    {
        if(objectToInspect != null)
        {
            defaultRotation = objectToInspect.rotation;
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
        if (objectToInspect != null)
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
                objectToInspect.rotation = rot * objectToInspect.rotation;

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
                    if(objectToInspect.rotation == defaultRotation)
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
    }

    public void SetBlendedRotation()
    {
        objectToInspect.rotation = Quaternion.RotateTowards(objectToInspect.rotation, defaultRotation, resetSpeed * Time.deltaTime);
    }
}
