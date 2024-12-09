using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimManager : MonoBehaviour
{
    public void TriggerNextEvent(GameObject self, Animator target, bool deactivate)
    {
        target.SetTrigger("StartTransition");

        if (deactivate)
        {
            self.SetActive(false);
        }
    }

    public void LoadNextScene()
    {
        gameObject.GetComponentInParent<MenuManager>().LoadNextScene();
    }
}
