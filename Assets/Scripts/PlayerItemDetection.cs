using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDetection : MonoBehaviour
{
    [SerializeField]
    private float desiredRadius;
    [SerializeField]
    private bool isAtRadius = false;

    [SerializeField]
    private CapsuleCollider col;
    [SerializeField]
    private HugoMovementTests playerScript;

    private void Start() 
    {
        if (col==null)
        {
            col = gameObject.GetComponent<CapsuleCollider>();
            col.radius = 0;
        }
        playerScript = gameObject.transform.GetComponentInParent<HugoMovementTests>(); 

    }

    private void Update() 
    {
        if (!isAtRadius && col != null)
        {
            col.radius += 0.05f;
            if (col.radius>=desiredRadius)
            {
                col.radius = desiredRadius;
                isAtRadius = true;
            }
        }
    }


    private void OnTriggerEnter(Collider c) 
    {
        
    }
}
