using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraMovement : MonoBehaviour
{
    private float speed;
    private bool loopTrue;
    private float maxSpeed;
    private float acceleration;
    
    void Start()
    {
        speed = 0f;
        loopTrue = false;
        maxSpeed = 0.9f;
        acceleration = 0.18f;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            loopTrue = true;  
        }

        if (loopTrue)
            {
                speed = Mathf.Lerp(speed, maxSpeed, acceleration * Time.deltaTime);
                transform.position += Vector3.forward * speed * Time.deltaTime;
            }
    }
}
