using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOpenDoorInteractable : InteractableBase
{

    public override void OnInteract()
    {
        base.OnInteract();

        Destroy(gameObject);
    }
}
