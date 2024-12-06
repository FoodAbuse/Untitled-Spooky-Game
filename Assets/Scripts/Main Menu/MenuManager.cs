using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject[] toBeHidden;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            ProgressToTitle();
        }
    }

    void ProgressToTitle()
    {
        foreach(GameObject g in toBeHidden)
        {
            g.SetActive(false);
        }
        
    }
}
