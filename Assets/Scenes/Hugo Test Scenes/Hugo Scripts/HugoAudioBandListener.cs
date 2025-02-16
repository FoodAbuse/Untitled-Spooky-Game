using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugoAudioBandListener : MonoBehaviour
{
    [Range(1, 8)]
    public int frequencyBand;
    [SerializeField]
    private HugoAudioVis audioBrain;

    
    public float liveValue;



    private void Start()
    {
        if (audioBrain==null)
        {
            audioBrain = GameObject.FindGameObjectWithTag("AudioFrequencyScribe").GetComponent<HugoAudioVis>();
        }
    }

    private void Update()
    {
        liveValue = UpdateFrequencyValue();
    }

    float UpdateFrequencyValue()
    {
        int index = frequencyBand - 1;
        float value = audioBrain.SendFreqValue(index);
        return value;
    }

    public float PassValue()
    {
        float value = liveValue;
        return value;
    }
}
