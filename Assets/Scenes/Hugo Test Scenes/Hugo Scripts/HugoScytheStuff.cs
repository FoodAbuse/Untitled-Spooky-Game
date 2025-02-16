using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugoScytheStuff : MonoBehaviour
{
    [SerializeField]
    private HugoAudioBandListener listener;

    [SerializeField]
    private Color lowValColour;
    [SerializeField]
    private Color highValColour;

    [SerializeField]
    private float lowValEmission;
    [SerializeField]
    private float highValEmission;
    [SerializeField]
    private float lowLightValue;
    [SerializeField]
    private float highLightValue;


    public Color liveColor;
    public float liveEmissionValue;
    public float liveLightValue;
    public float liveValue;

    public int materialIndex = 2;

    public Light pointLight;

    private void Start()
    {
        listener = gameObject.GetComponent<HugoAudioBandListener>();
    }
    private void Update()
    {
        UpdateEmission();
        AssignMaterialValues();

    }


    private void UpdateEmission()
    {
        float value = listener.liveValue;
        liveColor = Color.Lerp(lowValColour, highValColour, value);
        liveValue = value;
        liveEmissionValue = Mathf.Lerp(lowValEmission, highValEmission, value);
        liveLightValue = Mathf.Lerp(lowLightValue, highLightValue, value);
    }

    private void AssignMaterialValues()
    {
        gameObject.GetComponent<Renderer>().materials[materialIndex].SetColor("_EmissionColour", liveColor);
        gameObject.GetComponent<Renderer>().materials[materialIndex].SetFloat("_EmissionStrength", liveEmissionValue);

        pointLight.intensity = liveLightValue;
    }
}
