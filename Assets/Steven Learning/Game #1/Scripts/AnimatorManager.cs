using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{

    public Animator animator;
    PlayerManager playerManager;
    PlayerLocomotion playerLocomotion;

    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerManager = GetComponentInParent<PlayerManager>();
        playerLocomotion = GetComponentInParent<PlayerLocomotion>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
    {
        //Animation Snapping
        float snappedVertical;
        float snappedHorizontal;

        #region Snapped Horizontal
            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                snappedHorizontal = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                snappedHorizontal = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                snappedHorizontal = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                snappedHorizontal = -1;
            }
            else
            {
                snappedHorizontal = 0;
            }
        #endregion

        #region Snapped Vertical
            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                snappedVertical = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            {
                snappedVertical = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                snappedVertical = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                snappedVertical = -1;
            }
            else
            {
                snappedVertical = 0;
            }
        #endregion

        if (isSprinting)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 2;
        }

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }

    public void PlayerTargetedAnimation(string targetAnimation, bool isInteracting, bool useRootMotion = false)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.SetBool("isUsingRootMotion", useRootMotion);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    private void OnAnimatorMove()
    {
        if (playerManager.isUsingRootMotion)
        {
            playerLocomotion.playerRigidbody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / Time.deltaTime;
            playerLocomotion.playerRigidbody.velocity = velocity;
        }
            
    }
}
