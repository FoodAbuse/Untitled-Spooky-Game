using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AutoAim : MonoBehaviour
{
    public Transform aimPosition;
    public GameObject currentWeapon;
    GameObject currentTarget;
    public float distance = 10f;

    bool isAiming;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon.transform.position = aimPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTarget();
        if(isAiming)
        {
            AutoAiming();
        }
    }

    private void CheckTarget()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            RaycastHit hit;

            if(Physics.Raycast(transform.position, transform.forward, out hit, distance))
            {
                if(hit.collider.CompareTag("Enemy"))
                {
                    Debug.Log("Target Found: " + hit.collider.name);
                    
                    currentTarget = hit.transform.gameObject;
                    isAiming = true;
                }
                else
                {
                    currentTarget = null;
                    isAiming = false;
                }
            }
        }
        else
        {
            isAiming = false;
            currentTarget = null;
        }
    }

    private void AutoAiming()
    {
        currentWeapon.transform.LookAt(currentTarget.transform);
    }
}
