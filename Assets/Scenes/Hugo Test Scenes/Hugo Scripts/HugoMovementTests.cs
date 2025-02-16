using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HugoMovementTests : MonoBehaviour
{
    [Header("Movement")]
    [Space(5)]

    [SerializeField]
    private float movementSpeed = 0.5f;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float rotationSpeed = 1f;
    public bool canMove;

    private float horizontalInput;
    private float verticalInput;


    public float horizontalBearing;


    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private Vector3 moveDirection;

    [Space(10)]
    public InputAction playerControls;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            currentSpeed = moveDirection.magnitude;
            //horizontalInput = Input.GetAxis("Horizontal");
            //verticalInput = Input.GetAxis("Vertical");

            moveDirection = new Vector3(playerControls.ReadValue<Vector2>().x, 0, playerControls.ReadValue<Vector2>().y);
            //moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

            //transform.position += moveDirection * movementSpeed * Time.deltaTime;
            transform.Translate(moveDirection * movementSpeed * Time.deltaTime, Space.World);

            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
