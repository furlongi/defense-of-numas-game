using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTower : MonoBehaviour
{
    public InventoryManager playerInventory;
    //public TowerCostData towerCostData;
    
    [NonSerialized] public BaseTower Tower;

    private Button _upgradeButton;
    private InventoryTracker _tracker; 

    
    void Start()
    {
        _upgradeButton = GetComponentInChildren<Button>();
        _upgradeButton.onClick.AddListener(PurchaseUpgrade);
        
        _tracker = GetComponentInChildren<InventoryTracker>();
    }

    private void PurchaseUpgrade()
    {
        int playerGreenCount = playerInventory.greenGem;
        int playerBlueCount = playerInventory.blueGem;
        int playerPurpleCount = playerInventory.purpleGem;
        int playerRedCount = playerInventory.redGem;
        
        int costGreenCount = Int32.Parse(_tracker.GreenCount.text);
        int costBlueCount = Int32.Parse(_tracker.BlueCount.text);
        int costPurpleCount = Int32.Parse(_tracker.PurpleCount.text);
        int costRedCount = Int32.Parse(_tracker.RedCount.text);
        
        bool canPurchase = (playerGreenCount >= costGreenCount) && (playerBlueCount >= costBlueCount) &&
                           (playerPurpleCount >= costPurpleCount) && (playerRedCount >= costRedCount);

        if (canPurchase)
        {
            playerInventory.GemTransaction(new int[] {costGreenCount, costBlueCount, costPurpleCount, costRedCount});
        }
    }
}
