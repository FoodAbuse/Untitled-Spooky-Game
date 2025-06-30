using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAnimation : MonoBehaviour
{
// this is the animator
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

// collider is the type of info fed into this area, other is the variable (other can be called anything u want)
    private void OnTriggerEnter(Collider other)
    {
        // is other a player?
        if (other.tag == "Player")
        {
            //if yes set off the trigger on the animator
            anim.SetTrigger("touched");
        }
    }
}
