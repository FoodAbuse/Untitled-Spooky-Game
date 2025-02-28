using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NarrativeStager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> narrativeSequence;
    [SerializeField]
    private int currentIndex;
    [SerializeField]
    private GameObject currentEnabledSequence;


    [SerializeField]
    private bool generateSequenceBasedOnChildObjects;

    [SerializeField]
    private bool timerActive;
    [SerializeField]
    private float timerUptime;

    [SerializeField]
    private float timerDuration;


    [SerializeField]
    private int nextSeqIndex;
    // Start is called before the first frame update
    void Start()
    {
        if (generateSequenceBasedOnChildObjects)
        {
            foreach(Transform c in transform.GetComponentsInChildren<Transform>())
            {
                // Ensures that the game object doesn't find itself in it's children, because unity names its methods stupidly 
                if(c.gameObject.name == gameObject.name)
                {
                    // go fuck yourself
                }

                // only adds objects that aren't itself :3
                else 
                {
                    c.gameObject.SetActive(false);
                    narrativeSequence.Add(c.gameObject);                    
                }
                
            }
        }

        // foreach (GameObject g in narrativeSequence)
        // {
        //     g.SetActive(false);
        // }

        currentIndex = 0;
    }

    void Update()
    {
        if (timerActive)
        {
            timerUptime+=Time.deltaTime;
            if(timerUptime>=timerDuration)
            {
                EnableSequenceAtIndex(nextSeqIndex);
                timerActive = false;
            }
        }
    }
    
    public void EnableSequenceAtIndex(int i)
    {
        // Disables a currently active sequence item in order to reset for later use
        if (currentEnabledSequence != null)
        {
            currentEnabledSequence.SetActive(false);
        }

        // Enables the desired sequence by the int value fed externally
        narrativeSequence[i].SetActive(true);
        currentEnabledSequence = narrativeSequence[i];
    }

    public void StartCountdownToNextSequence(float duration)
    {
        timerDuration = duration;
        timerUptime = 0;
        timerActive = true;
        
    }

    public void SetNextIndex(int index)
    {
        nextSeqIndex = index;
    }
}
