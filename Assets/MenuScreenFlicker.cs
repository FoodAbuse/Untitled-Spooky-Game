using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenFlicker : MonoBehaviour
{
    public AudioSource[] audioSources;
    private bool isPlayingLight;
    private Light lightSource;
    void Start()
    {
        isPlayingLight = false;
        lightSource = GetComponent<Light>();
        lightSource.enabled = false;
        lightSource.intensity = 0.34f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !isPlayingLight)
        {
            StartCoroutine(StartFlickers());
        }
    }

    public IEnumerator StartFlickers()
    {
        isPlayingLight = true;
        audioSources[3].Play();
        yield return new WaitForSeconds(1);
        lightSource.enabled = true;
        audioSources[0].Play();
        yield return new WaitForSeconds(0.1f);
        lightSource.enabled = false;
        yield return new WaitForSeconds(0.65f);
        lightSource.enabled = true;
        lightSource.intensity = 1.55f;
        audioSources[1].Play();
        yield return new WaitForSeconds(0.1f);
        lightSource.enabled = false;
        yield return new WaitForSeconds(0.15f);
        lightSource.enabled = true;
        lightSource.intensity = 6.36f;
        audioSources[2].Play();
        //audioSources[4].Play();  being the light whoosh
        //6.36
    }
}
