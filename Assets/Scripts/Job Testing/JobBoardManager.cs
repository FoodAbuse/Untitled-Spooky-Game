using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobBoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Assigns an index to each job present
        int index = 1;
        foreach (JobOption job in transform.GetComponentsInChildren<JobOption>())
        {
            job.jobIndex = index;
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed(int i)
    {

    }
}
