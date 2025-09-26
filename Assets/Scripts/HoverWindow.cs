using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject hoverWindow;

    void Start()
    {
        hoverWindow.SetActive(false);
    }

    public void HoverWindowEnable()
    {
        hoverWindow.SetActive(true);
    }

    public void HoverWindowDisable()
    {
        hoverWindow.SetActive(false);
    }
}
