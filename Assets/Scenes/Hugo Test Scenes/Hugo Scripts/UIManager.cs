using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private bool fadeInOnLoad;

    [SerializeField]
    private UIBlinders blinders;

    // Start is called before the first frame update
    void Start()
    {
        if (fadeInOnLoad && blinders!=null)
        {
            blinders.gameObject.SetActive(true);
            blinders.SetAnimBoolTrue();
        }
        else
        {
            if (blinders!=null)
            {
                blinders.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
