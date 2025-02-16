using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HugoAudioVis : MonoBehaviour
{
    private float[] samples = new float[512];
    public float[] averagedSamples = new float[8];
    public GameObject[] sampleOutput;
    public AudioSource audioSource;

    public float[] topValue = new float[8];
    public float[] botValue = new float[8];

    public static float[] freqBand = new float[8];
    void Update()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        averagedSamples = AverageSampleStream(samples);
        SortFrequencyBands();
        
        

        for (int i = 0; i < 8; i++)
        {
            if (averagedSamples[i] >= topValue[i])
            {
                topValue[i] = averagedSamples[i];
            }
            if (averagedSamples[i] <= botValue[i])
            {
                botValue[i] = averagedSamples[i];
            }
            if (botValue[i] == 0)
            {
                botValue[i] = averagedSamples[i];
            }

            sampleOutput[i].GetComponent<TMPro.TextMeshProUGUI>().text = (freqBand[i]).ToString();     
        }
    }

    private float[] AverageSampleStream(float[] samplesIn)
    {
        float[] samplesOut = new float[8];

        for (int i=0; i<512; i++)
        {
            if (i<64)
            {
                samplesOut[0] +=samplesIn[i];
                
            }
            if (i>63 && i<128)
            {
                samplesOut[1] += samplesIn[i];
                
            }
            if (i > 127 && i < 192)
            {
                samplesOut[2] += samplesIn[i];
                
            }
            if (i > 191 && i < 256)
            {
                samplesOut[3] +=samplesIn[i];
                
            }
            if (i > 255 && i < 320)
            {
                samplesOut[4] += samplesIn[i];
                
            }
            if (i > 319 && i < 384)
            {
                samplesOut[5] += samplesIn[i];
                
            }
            if (i > 383 && i < 448)
            {
                samplesOut[6] += samplesIn[i];
                //Debug.Log("i > 47 && i < 56 flagged "+i);
            }
            if (i > 447 && i <= 512)
            {
                samplesOut[7] += samplesIn[i];
                //Debug.Log(i+" - "+samplesOut[7]);
            }
        }

        for (int i=0; i<8; i++)
        {
            samplesOut[i] = samplesOut[i] / 64;
            //Debug.Log(i + " is current index, Starting value is " + samplesOut[i]);
            float x = samplesOut[i];
            samplesOut[i] = x * 10000000;
            //Debug.Log(i + " is current index, unrounded adjusted value is " + samplesOut[i]);
            x = samplesOut[i];
            x = x * 1000;
            x = Mathf.Round(x);
            x = x * 0.001f;
            samplesOut[i] = x;

            //Debug.Log(i + " is current index, final exported value is " + samplesOut[i]);
        }

        return samplesOut;
    }

    private void SortFrequencyBands()
    {
        int count = 0;

        for (int i=0;i<8;i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i==7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j <sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBand[i] = average * 10;

        }
    }
    public float SendFreqValue(int i)
    {
        float value = freqBand[i];
        return value;
    }
}
