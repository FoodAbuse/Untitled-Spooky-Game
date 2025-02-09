using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = transform.Find("itemSlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 75f;

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransoform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransoform.gameObject.SetActive(true);
            itemSlotRectTransoform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransoform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
            if(x > 4)
            {
                x = 0;
                y++;
            }
        }
    }
}
