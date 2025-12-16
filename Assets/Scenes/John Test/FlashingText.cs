using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour
{
    [SerializeField]
    private GameObject FlashingTextObject;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private float flashSpeed;

    private bool hasKeyPressed;

    void Start()
    {
        hasKeyPressed = false;
        if (flashSpeed <= 0f)
        {
            flashSpeed = 0.5f;
        }

        StartCoroutine(FlashTimer());
    }

    void Update()
    {
        /*
        while (!hasKeyPressed)
        {
            canvasGroup.alpha = 1f;
            StartCoroutine(FlashTimer());
            canvasGroup.alpha = 0f;
            StartCoroutine(FlashTimer());
        }
        */
        if (Input.anyKeyDown)
        {
            hasKeyPressed = true;
            Destroy(gameObject);
        }
    }
    
    private IEnumerator FlashTimer()
    {

        while (!hasKeyPressed)
        {
            canvasGroup.alpha = 1f;
            yield return new WaitForSeconds(flashSpeed);
            
            canvasGroup.alpha = 0f;
            yield return new WaitForSeconds(flashSpeed);
        }

    }
}
