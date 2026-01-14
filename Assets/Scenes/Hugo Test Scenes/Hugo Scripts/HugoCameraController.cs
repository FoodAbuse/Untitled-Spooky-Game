using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugoCameraController : MonoBehaviour
{
    public enum CameraType{Static=0, Dynamic=1, Stationary=2}

    [SerializeField]
    private CameraType cameraType;

    public Transform playerTransform;
    public Transform cameraTransform;
    public Transform cameraRotationTransform;

    public float cameraSwivelSpeed = 1;
    public float cameraVerticalSpeed = 1;
    public float verticalOffset;
    public Vector3 playerStartingPos;
    public Vector3 cameraStartingPos;


    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (cameraRotationTransform == null)
        {
            cameraRotationTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
        if (cameraTransform == null)
        {
            cameraTransform = gameObject.transform;
        }

        verticalOffset = cameraTransform.position.y - playerTransform.position.y;
        playerStartingPos = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);
        cameraStartingPos = new Vector3(cameraTransform.position.x, cameraTransform.position.y, cameraTransform.position.z);

    }

    private void Update()
    {
        if (cameraType == CameraType.Static)
        {
            Vector3 relativePosition = playerTransform.position - cameraRotationTransform.position;
            Quaternion targetRotation = Quaternion.LookRotation(relativePosition, Vector3.up);

            //cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, playerTransform.position.z);
            cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, playerTransform.position.z);
            cameraRotationTransform.rotation = Quaternion.Lerp(cameraRotationTransform.rotation, targetRotation, Time.deltaTime * cameraSwivelSpeed);
        }
        if(cameraType == CameraType.Dynamic)
        {
            Vector3 relativePosition = playerTransform.position - cameraRotationTransform.position;
            Quaternion targetRotation = Quaternion.LookRotation(relativePosition, Vector3.up);

            //cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, playerTransform.position.z); // follows on z axis only
            //cameraTransform.position = new Vector3(playerTransform.position.x, cameraTransform.position.y, playerTransform.position.z); // follows on x and z axis


            if(playerTransform.position.y>playerStartingPos.y) //if player's vertical position has increased beyond what it started as
            {
                Vector3 currentPos = new Vector3(playerTransform.position.x, cameraTransform.position.y, playerTransform.position.z);
                Vector3 targetPos = new Vector3(playerTransform.position.x, (playerTransform.position.y + verticalOffset), playerTransform.position.z);
                cameraTransform.position = Vector3.Lerp(currentPos, targetPos, Time.deltaTime * cameraVerticalSpeed); //move the camera up to the same relative height at a rate determined by cameraVerticalSpeed
            }
            else
            {
                cameraTransform.position = new Vector3(playerTransform.position.x, cameraTransform.position.y, playerTransform.position.z);
            }
            //cameraTransform.position = new Vector3(playerTransform.position.x, (playerTransform.position.y + verticalOffset), playerTransform.position.z); // follows on all axis
            cameraRotationTransform.rotation = Quaternion.Lerp(cameraRotationTransform.rotation, targetRotation, Time.deltaTime * cameraSwivelSpeed);
        }
        if(cameraType == CameraType.Stationary)
        {
            Vector3 relativePosition = playerTransform.position - cameraRotationTransform.position;
            Quaternion targetRotation = Quaternion.LookRotation(relativePosition, Vector3.up);
            cameraRotationTransform.rotation = Quaternion.Lerp(cameraRotationTransform.rotation, targetRotation, Time.deltaTime * cameraSwivelSpeed);
        }
    }

    private float CameraToPlayerRelativeYPos()
    {
        float relativeGap = playerTransform.position.y - playerStartingPos.y + verticalOffset;
        return relativeGap;
    }
}


