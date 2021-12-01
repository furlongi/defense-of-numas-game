using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerShop : MonoBehaviour
{
    private List<TowerShopItem> _shopItems;
    public InventoryManager playerInventory;
    public GameObject cancelItemDragging;
    public TowerList towerList;
    
    void Start()
    {
        _shopItems = GetComponentsInChildren<TowerShopItem>().ToList();
        if (playerInventory == null)
        {
            playerInventory = GameObject.Find("Player Inventory").GetComponent<InventoryManager>();
        }
    }

    public bool PurchaseTower(InventoryTracker itemCost)
    {
        int playerGreenCount = playerInventory.greenGem;
        int playerBlueCount = playerInventory.blueGem;
        int playerPurpleCount = playerInventory.purpleGem;
        int playerRedCount = playerInventory.redGem;

        int costGreenCount = Int32.Parse(itemCost.GreenCount.text);
        int costBlueCount = Int32.Parse(itemCost.BlueCount.text);
        int costPurpleCount = Int32.Parse(itemCost.PurpleCount.text);
        int costRedCount = Int32.Parse(itemCost.RedCount.text);

        bool canPurchase = (playerGreenCount >= costGreenCount) && (playerBlueCount >= costBlueCount) &&
                           (playerPurpleCount >= costPurpleCount) && (playerRedCount >= costRedCount);
        if (canPurchase)
        {
            // playerInventory.GreenCount.text = (playerGreenCount - costGreenCount).ToString();
            // playerInventory.BlueCount.text = (playerBlueCount - costBlueCount).ToString();
            // playerInventory.PurpleCount.text = (playerPurpleCount - costPurpleCount).ToString();
            // playerInventory.RedCount.text = (playerRedCount - costRedCount).ToString();
            playerInventory.GemTransaction(new int[] {costGreenCount, costBlueCount, costPurpleCount, costRedCount});
            return true;
        }
        return false;
    }
}
