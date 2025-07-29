using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugoInteractable : MonoBehaviour
{
    [SerializeField]
    private SphereCollider interactionRange;
    [SerializeField]
    private int interestPriority;
    [SerializeField]
    private bool inRange;


    [SerializeField]
    private enum InteractableType {Door, Pickup, Switch}
    [SerializeField]
    private InteractableType interactableType;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name + ": Priority "+interestPriority);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        HugoMovementTests player = collision.gameObject.GetComponent<HugoMovementTests>();
        if (collision.gameObject.tag == "Player" && player!=null)
        {
            Debug.Log(gameObject.name + ": Player entered range");
            //pass info to player script
            ToggleOutline(true);
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        HugoMovementTests player = collision.gameObject.GetComponent<HugoMovementTests>();
        if (collision.gameObject.tag == "Player" && player != null)
        {
            ToggleOutline(false);
            inRange = false;
        }
    }
    void ToggleOutline(bool val)
    {
        if(val==true) //if bool is true, create outline
        {
            Debug.Log("Enabling outline on "+gameObject.name);
        }
        else //else, remove outline
        {
            Debug.Log("Disabling outline on " + gameObject.name);
        }
    }

    public void ObjectClicked(HugoMovementTests player)
    {
        //check what item type this is and redirect accordingly
        Debug.Log(gameObject.name + " was Selected");
        if(interactableType==InteractableType.Door)
        {
            gameObject.GetComponent<DoorLogic>().UseDoor(player.gameObject);
        }
    }
}
