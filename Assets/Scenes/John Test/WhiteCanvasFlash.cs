using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteCanvasFlash : MonoBehaviour
{
    [SerializeField]
    private GameObject newMenuScreen;
    [SerializeField]
    private CanvasGroup canvasGroup;

    private float flashSpeed;

    void Start()
    {
        flashSpeed = 1f;

        newMenuScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(WhiteScreenFlash());
        }
    }

    private IEnumerator WhiteScreenFlash()
    {
        yield return new WaitForSeconds(2f);

        float time = 0f;
        while (time < flashSpeed)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(time / flashSpeed);
            yield return null;
        }

        newMenuScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        time = 0f;
        while (time < flashSpeed)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1 - (time / flashSpeed));
            yield return null;
        }

        Destroy(gameObject);
    }
}
