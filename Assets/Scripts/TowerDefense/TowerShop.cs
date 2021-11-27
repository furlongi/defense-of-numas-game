using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerShop : MonoBehaviour
{
    private List<TowerShopItem> _shopItems;
    public InventoryTracker playerInventory;
    public GameObject cancelItemDragging;
    void Start()
    {
        _shopItems = GetComponentsInChildren<TowerShopItem>().ToList();
        if (playerInventory == null)
        {
            playerInventory = GameObject.Find("Player Inventory").GetComponent<InventoryTracker>();
        }
    }

    public bool PurchaseTower(InventoryTracker itemCost)
    {
        int playerGreenCount = Int32.Parse(playerInventory.GreenCount.text);
        int playerBlueCount = Int32.Parse(playerInventory.BlueCount.text);
        int playerPurpleCount = Int32.Parse(playerInventory.PurpleCount.text);
        int playerRedCount = Int32.Parse(playerInventory.RedCount.text);

        int costGreenCount = Int32.Parse(itemCost.GreenCount.text);
        int costBlueCount = Int32.Parse(itemCost.BlueCount.text);
        int costPurpleCount = Int32.Parse(itemCost.PurpleCount.text);
        int costRedCount = Int32.Parse(itemCost.RedCount.text);

        bool canPurchase = (playerGreenCount >= costGreenCount) && (playerBlueCount >= costBlueCount) &&
                           (playerPurpleCount >= costPurpleCount) && (playerRedCount >= costRedCount);
        if (canPurchase)
        {
            playerInventory.GreenCount.text = (playerGreenCount - costGreenCount).ToString();
            playerInventory.BlueCount.text = (playerBlueCount - costBlueCount).ToString();
            playerInventory.PurpleCount.text = (playerPurpleCount - costPurpleCount).ToString();
            playerInventory.RedCount.text = (playerRedCount - costRedCount).ToString();
            return true;
        }
        return false;

    }
    
}
