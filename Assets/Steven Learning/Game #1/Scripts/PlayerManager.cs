using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private UIInventory uiInventory;

    Animator animator;
    PlayerLocomotion playerLocomotion;
    CameraManager cameraManager;
    InputManager inputManager;
    Inventory inventory;
    InteractionController interactionController;

    public bool isInteracting;
    public bool isUsingRootMotion;

    private void Awake()
    {
        playerLocomotion = GetComponent<PlayerLocomotion>();
        cameraManager = FindObjectOfType<CameraManager>();
        inputManager = GetComponent<InputManager>();
        animator = GetComponent<Animator>();
        interactionController = GetComponent<InteractionController>();

        inventory = new Inventory();
        uiInventory.SetInventory(inventory);

        ItemWorld.SpawnItemWorld(new Vector3(20, 20), new Item {itemType = Item.ItemType.Cube, itemAmount = 1});
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();

        isInteracting = animator.GetBool("isInteracting");
        isUsingRootMotion = animator.GetBool("isUsingRootMotion");
        playerLocomotion.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerLocomotion.isGrounded);
        
    }
}
