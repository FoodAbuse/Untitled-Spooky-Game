using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interaction Data", menuName = "InteractionSystem/InteractionData")]
public class InteractionData : ScriptableObject
{
    private InteractableBase m_interactable;

    public InteractableBase Interactable
    {
        get => m_interactable;
        set => m_interactable = value;
    }

    public void Interact()
    {
        m_interactable.OnInteract();
        ResetData();
    }

    public bool IsSameInteractable(InteractableBase _newInteractable)
    {
        return m_interactable == _newInteractable;
    }

    public void ResetData()
    {
        m_interactable = null;
    }

    public bool IsEmpty() 
    {
        return m_interactable == null;
    }
}
