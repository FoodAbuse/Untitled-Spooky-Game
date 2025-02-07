using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;


    public Transform targetTrasnform; //this is the target that the camera will follow
    public Transform cameraPivot; //this is the pivot that the camera will rotate around
    public Transform cameraTransform; //The trasnform of the actual camera objectt in the scene
    public LayerMask collisionLayers; //the layers that the camera will collide with
    private float defaultPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;

    public float cameraCollisionOffset = 0.2f; //How much the camera will jump off of objects when it colides
    public float minimumCollisionOffset = 0.2f; //The minimum distance the camera will be from the player
    public float cameraCollisionRadius = 0.2f;
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2;
    public float cameraPivotSpeed = 2;
    

    public float lookAngle; //Camera looking up and down
    public float pivotAngle; //Camera looking left and right
    public float minimumPivot = -35;
    public float maximumPivot = 35;

    public bool fixedCamera;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTrasnform = FindObjectOfType<PlayerManager>().transform;
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp
            (transform.position, targetTrasnform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }

    private void RotateCamera()
    {

        if (fixedCamera == false) 
            {
                Vector3 rotation;
                Quaternion targetRotation;

                lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
                pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);  
                pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

                rotation = Vector3.zero;
                rotation.y = lookAngle;
                targetRotation = Quaternion.Euler(rotation);
                transform.rotation = targetRotation;

                rotation = Vector3.zero;
                rotation.x = pivotAngle;
                targetRotation = Quaternion.Euler(rotation);
                cameraPivot.localRotation = targetRotation;
            }
    }

    private void HandleCameraCollisions()
    {
        //this will handle camera collisions

        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast
            (cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))

        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition =- (distance - cameraCollisionOffset);
        }

        if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition = targetPosition - minimumCollisionOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
