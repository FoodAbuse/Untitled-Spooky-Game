using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpInteractable : InteractableBase
{
    Inventory inventory;
    public ItemWorld itemWorld;

    private void Awake()
    {
        itemWorld = GetComponent<ItemWorld>();
    }

    public override void OnInteract()
    {
        base.OnInteract();

        inventory.AddItem(itemWorld.GetItem());
        Destroy(gameObject);
    }
}
