using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencedCameraComponent : MonoBehaviour
{
    [SerializeField]
    private int sequenceIndex;

    private void Start()
    {
        if(gameObject.tag!="MainCamera")
        {
            gameObject.SetActive(false);
        }
    }


    public void UpdateIndex(int i)
    {
        sequenceIndex = i;
    }

    public int RetrieveIndex()
    {
        return sequenceIndex;
    }

    public void SetMainCamera()
    {
        GameObject previousCamera = Camera.main.gameObject;
        previousCamera.tag = ("Untagged");
        previousCamera.SetActive(false);
        gameObject.tag = ("MainCamera");
    }
}
