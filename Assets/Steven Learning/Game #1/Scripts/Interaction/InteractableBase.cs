using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    #region Variables
    [Header("Interactable Settings")]

    public float holdDuration;

    [Space]
    public bool holdInteract;
    public bool multipleUse;
    public bool isInteractable;
    [SerializeField] private string tooltipMessage = "interact";
    [SerializeField] private string notInteractableToolTipMessage = "not Interactable";

    public float HoldDuration => holdDuration;
    #endregion

    #region Properties 
    public bool HoldInteract => holdInteract;
    public bool MultipleUse => multipleUse;
    public bool IsInteractable => isInteractable;
    public string TooltipMessage => tooltipMessage;
    public string NotInteractableToolTipMessage => notInteractableToolTipMessage;
    #endregion
    
    #region Methods
    public virtual void OnInteract()
    {
        Debug.Log("Interacted: " + gameObject.name);
    }
    #endregion
    
}
