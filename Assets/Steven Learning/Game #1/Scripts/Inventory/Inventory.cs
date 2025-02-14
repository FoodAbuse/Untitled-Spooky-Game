using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    private List<Item> itemList;

    public event EventHandler OnItemListChanged;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item{ itemType = Item.ItemType.Cube, itemAmount = 1});

        
        //AddItem(new Item{ itemType = Item.ItemType."ItemName", itemAmount = 1});

        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
