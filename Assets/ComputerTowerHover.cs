using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTowerHover : MonoBehaviour
{
    [Header("Hover Up/Down Floats")]
    public float height = 0.01f;
    public float speed = 0.1f;

    [Header("Hover Rotation Floats")]
    public float rotationHeight = 15f;
    public float rotationSpeed = 0.3f;
    
    private Vector3 startingPosition;
    private Quaternion startingRotation;

    void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;
    }


    void Update()
    {
        //hover code
        float yOffset = Mathf.Sin(Time.time * speed) * height;
        Vector3 pos = startingPosition + Vector3.up * yOffset;

        //rotation code
        float yRotation = Mathf.Sin(Time.time * rotationSpeed) * rotationHeight;
        Quaternion rot = startingRotation * Quaternion.Euler(yRotation, 0f, 0f);

        transform.SetPositionAndRotation(pos, rot);
    }
}
