using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBlackFadeOut : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup blackFadeOutCanvasGroup;
    [SerializeField]
    private float fadeDuration;
    [SerializeField]
    private Canvas canvasBlack;

    private bool hasPlayerSpawned;

    void Start()
    {
        if (fadeDuration < 1f)
        {
            fadeDuration = 1f;
        }

        blackFadeOutCanvasGroup.alpha = 0f;
        canvasBlack.sortingOrder = 99;
    }

    public void StartTheSequence()
    {
        StartCoroutine(UIBlackFadeSequence());
    }

    public IEnumerator UIBlackFadeSequence()
    {
        canvasBlack.sortingOrder = 101;
        blackFadeOutCanvasGroup.alpha = 0f;

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            blackFadeOutCanvasGroup.alpha = Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }

        blackFadeOutCanvasGroup.alpha = 1f;

        hasPlayerSpawned = true;
    }
}
