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

            //cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, playerTransform.position.z);
            cameraTransform.position = new Vector3(playerTransform.position.x, cameraTransform.position.y, playerTransform.position.z);
            cameraRotationTransform.rotation = Quaternion.Lerp(cameraRotationTransform.rotation, targetRotation, Time.deltaTime * cameraSwivelSpeed);
        }
        if(cameraType == CameraType.Stationary)
        {
            Vector3 relativePosition = playerTransform.position - cameraRotationTransform.position;
            Quaternion targetRotation = Quaternion.LookRotation(relativePosition, Vector3.up);
            cameraRotationTransform.rotation = Quaternion.Lerp(cameraRotationTransform.rotation, targetRotation, Time.deltaTime * cameraSwivelSpeed);
        }
    }
}
