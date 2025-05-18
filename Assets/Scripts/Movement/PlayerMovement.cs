using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Variables")]
    private CharacterController controller;
    Vector3 movDir;

    [Space]
    [Header("Movement Flags")]
    public bool isSprinting;

    [Space]
    [Header("Movement")]
    public float walkingSpeed = 5f;
    public float runningSpeed = 10f;
    public float turnSpeed = 100f;
    public float targetSpeed;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        movDir = transform.forward * Input.GetAxis("Vertical") * targetSpeed;

        controller.Move(movDir * Time.deltaTime - Vector3.up * 0.1f);
    }

    private float HandleMovement()
    {

        isSprinting = Input.GetKey(KeyCode.LeftShift);

            if (isSprinting)
            {
                targetSpeed = runningSpeed;
            }
            else
            {
                targetSpeed = walkingSpeed;
            }

            return targetSpeed;
    }
}
