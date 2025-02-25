using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

public class InteractionController : MonoBehaviour
{
    #region Variables
        [Header("Data")]
        public InteractionInputData interactionInputData;
        public InteractionData interactionData;
        public InputManager inputManager;

        [Space, Header("UI")]
        [SerializeField] private InteractionUIPanel uIPanel;

        [Space]
        [Header("Ray Settings")]
        public float rayDistance;
        public float raySphereRadius;
        public LayerMask interactableLayer;

        [Space]
        [Header("Interact Flags")]
        public bool isObjInteracting;

    #endregion

    #region Private
        private Camera m_cam;
        private bool m_interacting;
        private float m_holdTimer = 0f;
        private InteractableBase _interactable;
    #endregion

    #region Built In Methods
        void Awake()
        {
            m_cam = FindObjectOfType<Camera>();
        }

        void Update()
        {
            CheckForInteractable();
            CheckForInteractableInput();
        }
    #endregion

    #region Custom Methods
        void CheckForInteractable()
        {
            Ray _ray = new Ray(m_cam.transform.position, m_cam.transform.forward);
            RaycastHit _hitInfo;

            bool _hitSomething = Physics.SphereCast(_ray, raySphereRadius, out _hitInfo, rayDistance, interactableLayer);
            if (_hitSomething)
            {
                _interactable = _hitInfo.transform.GetComponent<InteractableBase>();

                if(_interactable != null)
                {
                    if(interactionData.IsEmpty())
                    {
                        interactionData.Interactable = _interactable;
                        uIPanel.SetTooltip(_interactable.TooltipMessage);
                    }
                    else
                    {
                        if(!interactionData.IsSameInteractable(_interactable))
                        {
                            interactionData.Interactable = _interactable;
                            uIPanel.SetTooltip(_interactable.TooltipMessage);
                        }
                    }
                }
            }
            else
            {
                uIPanel.RestUI();
                interactionData.ResetData();
            }

            Debug.DrawRay(_ray.origin,_ray.direction * rayDistance, _hitSomething ? Color.green : Color.red);
        }
        void CheckForInteractableInput()
        {
            if(interactionData.IsEmpty())
                return;

            if(inputManager.interactedInputClicked)
            {
                m_interacting = true;
                m_holdTimer = 0f;
            }
            if(inputManager.interactedInputReleased)
            {
                m_interacting = false;
                m_holdTimer = 0f;
                uIPanel.UpdateProgressBar(0f);
            }

            if(m_interacting)
            {
                if(!interactionData.Interactable.IsInteractable)
                    return;

                if(interactionData.Interactable.HoldInteract)
                {
                    m_holdTimer += Time.deltaTime;

                    float heldPercent = m_holdTimer / interactionData.Interactable.HoldDuration;
                    uIPanel.UpdateProgressBar(heldPercent);

                    if(heldPercent > 1f)
                    {
                        if(_interactable.interactionType == InteractableBase.InteractionType.Item)
                        {
                            interactionData.Interact(_interactable.GetComponent<ItemWorld>());
                        }
                        else
                        {
                            interactionData.Interact();
                            m_interacting = false;
                        }

                         m_interacting = false;
                    }
                    
                }
                else
                {
                    if(_interactable.interactionType == InteractableBase.InteractionType.Item)
                        {
                            interactionData.Interact(_interactable.GetComponent<ItemWorld>());
                        }
                    else
                        {
                            interactionData.Interact();
                            m_interacting = false;
                        }

                    m_interacting = false;
                    
                }
            }
        }
    #endregion
}
