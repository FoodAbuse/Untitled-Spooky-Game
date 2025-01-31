using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;
    InteractionController interactionController;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX; //this is the x axis of the camera
    public float cameraInputY; //this is the y axis of the camera

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool shiftInput;

    public bool jumpInput;

    public bool dodgeInput;

    public bool interactedInputClicked;
    public bool interactedInputReleased;

    #region Data

        [SerializeField] InteractionInputData interactionInputData;

    #endregion

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        interactionController = Camera.main.transform.parent.parent.gameObject.GetComponent<InteractionController>();
        interactionInputData.ResetInput();
    }
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Shift.performed += i => shiftInput = true;
            playerControls.PlayerActions.Shift.canceled += i => shiftInput = false;

            playerControls.PlayerActions.Jump.performed += i => jumpInput = true;

            playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;

            //THIS FUNCTION IS DISABLED BECUASE IT'S NOT WOKRING AS INTENDED - THE PRESS INPUT WOULD TRIGGER TO MANY TIMES
            playerControls.PlayerActions.Interact.performed += i => interactedInputClicked = true;
            playerControls.PlayerActions.Interact.canceled += i => interactedInputReleased = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        HandleDodgeInput();
        HandleInteractInput();
        //HandleAttackInput();
        
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSprintingInput()
    {
        if (shiftInput && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            playerLocomotion.HandleJumping();
        }
    }

    private void HandleDodgeInput()
    {
        if (dodgeInput)
        {
            dodgeInput = false;
            playerLocomotion.HandleDodge();
        }
    }

    private void HandleInteractInput()
    {
        if (interactedInputClicked)
        {
            interactionController.isObjInteracting = true;
            interactedInputClicked = false;
        }

        if (interactedInputReleased)
        {
            interactionController.isObjInteracting = false;
            interactedInputReleased = false;
        }
    }


    // private void GetInteractionInputData()
    // {
    //     interactionInputData.InteractedClicked = Input.GetKeyDown(KeyCode.E);
    //     Debug.Log("E Clicked" + interactionInputData.InteractedClicked);
    //     interactionInputData.InteractedReleased = Input.GetKeyUp(KeyCode.E);
    //     Debug.Log("E Released" + interactionInputData.InteractedReleased);
    // }
}
