using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverFunctionality : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private AudioSource hoverAudio;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverAudio != null)
        {
            //hoverAudio.Play();
        }
    }
}
