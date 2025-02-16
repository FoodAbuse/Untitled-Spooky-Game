using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HugoVisualiser : MonoBehaviour
{
    private HugoAudioBandListener listener;
    [SerializeField]
    private Slider slider;

    float valueMultiplier = 1;

    private void Start()
    {
        listener = gameObject.GetComponent<HugoAudioBandListener>();
        slider = gameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = listener.PassValue() * valueMultiplier;
    }


}
