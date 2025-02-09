using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        Cube,
    }

    public ItemType itemType;
    public int itemAmount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Cube: return ItemAssets.Instance.cubeItemSprite;
            // TO MAKE MORE ITEMS:
            // case ItemType."ItemName": return ItemAssets.Instance."ItemName";
        }
    }

    public Transform GetTransform()
    {
        switch (itemType)
        {
            default:
            case ItemType.Cube: return ItemAssets.Instance.cubeItemPF;
            // TO MAKE MORE ITEMS:
            // case ItemType."ItemName": return ItemAssets.Instance."ItemName";
        }
    }
}
