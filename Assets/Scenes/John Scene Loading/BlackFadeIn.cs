using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackFadeIn : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup blackFadeOutCanvasGroup;
    [SerializeField]
    private float fadeDuration;

    private bool hasPlayerSpawned;

    void Start()
    {
        if (fadeDuration < 1f)
        {
            fadeDuration = 1f;
        }


        hasPlayerSpawned = false;
        if (!hasPlayerSpawned)
        { 
            StartCoroutine(UIBlackFadeSequence());
        }
    }

    private IEnumerator UIBlackFadeSequence()
    {
        blackFadeOutCanvasGroup.alpha = 1f;

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            blackFadeOutCanvasGroup.alpha = Mathf.Clamp01(1f - (time / fadeDuration));
            yield return null;
        }

        blackFadeOutCanvasGroup.alpha = 0f;

        hasPlayerSpawned = true;
        
    }
}
