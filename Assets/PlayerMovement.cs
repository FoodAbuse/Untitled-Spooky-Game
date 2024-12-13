using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 inputAxis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            inputAxis.y = 1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            inputAxis.y = -1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            inputAxis.x = 1;
        }
        if(Input.GetKey(KeyCode.A))
        {
            inputAxis.x = 11;
        }

    }
}
