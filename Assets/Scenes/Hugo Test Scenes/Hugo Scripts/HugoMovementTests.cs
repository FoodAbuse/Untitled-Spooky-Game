using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HugoMovementTests : MonoBehaviour
{
    [Header("Movement")]
    [Space(5)]

    //[SerializeField]
    //private float movementSpeed = 0.5f;
    [SerializeField]
    private float activeSpeed;

    [SerializeField]
    private float animSpeedMultiplier;
    [SerializeField]
    private float runAnimSpeed = 1f;
    [SerializeField]
    private float walkAnimSpeed = 1f;
    [SerializeField]
    private float accelerationSpeed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float rotationSpeed = 1f;

    [SerializeField]
    private float sprintMultiplier = 1.5f;
    public bool canMove;

    private float horizontalInput;
    private float verticalInput;


    public float horizontalBearing;

    [SerializeField]
    private float movementAnimThreshold;


    [SerializeField]
    private float currentMagnitude;
    [SerializeField]
    private float currentVelocity;
    
    [SerializeField]
    private Vector3 moveDirection;

    [Header("Player Stuff")]
    [Space(10)]
    public InputAction playerControls;

    [Space(5)]
    [SerializeField]
    private Transform playerHeadJoint;

    [SerializeField]
    private bool isOutside;

    private Rigidbody rb;

    [Space(10)]
    [Header("World Interaction Stuff")]
    [SerializeField]
    private bool isStaring;

    [SerializeField]
    private float stareDelay;
    [SerializeField]
    private float delayTimer;

    [SerializeField]
    private  List<GameObject> interestingObjs;
    [SerializeField]
    private Transform stareTarget;


    [SerializeField]
    private enum AnimationState{IndoorSleepy = 0, Indoor = 1, Outdoor = 2}

    private AnimationState animationState = new AnimationState();


    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start() 
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (isOutside)
        {
             gameObject.GetComponentInChildren<Animator>().SetBool("IsOutside", true);
        }
        else
        {
             gameObject.GetComponentInChildren<Animator>().SetBool("IsOutside", false);
        }
    }
    void Update()
    {
        currentVelocity = Vector3.Magnitude(rb.velocity);
        if (canMove)
        {
            currentMagnitude = moveDirection.magnitude;
            //horizontalInput = Input.GetAxis("Horizontal");
            //verticalInput = Input.GetAxis("Vertical");

            moveDirection = new Vector3(playerControls.ReadValue<Vector2>().x, 0, playerControls.ReadValue<Vector2>().y);



            if (playerControls.ReadValue<Vector2>().x != 0 || playerControls.ReadValue<Vector2>().y != 0)
            {
                float speedMultiplier = 1f;
                float targetSpeed = 1f;
                if (Keyboard.current[Key.LeftShift].isPressed)
                {
                    speedMultiplier = sprintMultiplier;
                    accelerationSpeed = sprintMultiplier * 2;
                    gameObject.GetComponentInChildren<Animator>().SetBool("IsSprinting", true);
                    animSpeedMultiplier = runAnimSpeed;
                }
                else 
                {
                    speedMultiplier = 1f;
                    accelerationSpeed = 2;
                    gameObject.GetComponentInChildren<Animator>().SetBool("IsSprinting", false);
                    animSpeedMultiplier = walkAnimSpeed;
                }

                targetSpeed = (speedMultiplier * maxSpeed);
                

                if (activeSpeed < targetSpeed)
                {
                    activeSpeed = activeSpeed + accelerationSpeed * Time.deltaTime;
                }
                else
                {
                    activeSpeed = targetSpeed;
                }
                
                
            }
            else
            {
                activeSpeed = 0;
            }
            //moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
            
            //transform.position += moveDirection * movementSpeed * Time.deltaTime;
            transform.Translate(moveDirection * activeSpeed * Time.deltaTime, Space.World);

            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
        if (activeSpeed != 0)
        {
            gameObject.GetComponentInChildren<Animator>().SetBool("IsIdle", false);
            gameObject.GetComponentInChildren<Animator>().speed = activeSpeed * animSpeedMultiplier;

        }
        else 
        {
            gameObject.GetComponentInChildren<Animator>().SetBool("IsIdle", true);
            gameObject.GetComponentInChildren<Animator>().speed = 1;
        }


        // Head Auto Stare 
        if (isStaring)
        {
            if (delayTimer>=stareDelay)
            {
                int index = 0;
                // Start looking at object
                if (interestingObjs.Count>1) // if we have more than a single object of interest
                {
                    index = Random.Range(1, interestingObjs.Count); // Gets an un-normalized index value
                    index -= 1; //normalizes value for true list index positions
                }


            }
            else
            {
                // keep waiting until timer is done
                delayTimer +=Time.deltaTime;
            }
            
        }


    }



    public void AddObjectOfInterest(GameObject g) 
    {
        interestingObjs.Add(g);
        stareDelay = Random.Range(0.1f, 0.9f);
        delayTimer = 0;
        isStaring = true;
    }

    private void VerifyNextStare()
    {
        
    }

    public void SwapAnimState(int index)
    {
        // if (index == ((int)AnimationState.IndoorSleepy))
        // {
        //     animationState = AnimationState.IndoorSleepy;
        // }
        // if (index == ((int)AnimationState.Indoor))
        // {
        //     animationState = AnimationState.Indoor;
        // }
        // if (index == ((int)AnimationState.Outdoor))
        // {
        //     animationState = AnimationState.Outdoor;
        // }

        // Recieve animation string instead, set this as a global event in game manager that changes multiple values
    }
}
