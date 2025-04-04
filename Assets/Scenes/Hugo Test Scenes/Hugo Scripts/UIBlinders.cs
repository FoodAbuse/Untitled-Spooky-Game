using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBlinders : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private string animBoolName;

    [SerializeField]
    private bool useCustomName;


    public void SetAnimBoolTrue()
    {
        if (useCustomName==true)
        {
            animator.SetBool(animBoolName, true);
        }
        else
        {
            animator.SetBool("default", true);
        }
    }
    public void SetAnimBoolFalse()
    {
        if (useCustomName==true)
        {
            animator.SetBool(animBoolName, false);
        }
        else
        {
            animator.SetBool("default", false);
        }
    }
}
