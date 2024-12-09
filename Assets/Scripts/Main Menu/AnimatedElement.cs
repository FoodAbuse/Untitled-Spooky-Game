using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedElement : MonoBehaviour
{
    [SerializeField]
    private bool disableOnEnd;

    [SerializeField]
    private Animator nextTarget;

    private MenuAnimManager animManager;
   void Start()
    {
        animManager = gameObject.GetComponentInParent<MenuAnimManager>();
    }

    public void AnimEnd()
    {
        animManager.TriggerNextEvent(gameObject, nextTarget, disableOnEnd);
    }
    
}
