using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTower : MonoBehaviour
{
    public InventoryManager playerInventory;
    
    private BaseTower _tower;

    private Button _upgradeButton;
    private InventoryTracker _tracker; 

    
    void Start()
    {
        _upgradeButton = GetComponentInChildren<Button>();
        _upgradeButton.onClick.AddListener(PurchaseUpgrade);
        
        _tracker = GetComponentInChildren<InventoryTracker>();
        gameObject.SetActive(false);
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
            _tower.UpgradeTower();
        }
    }

    public void SetTower(BaseTower tower)
    {
        _tower = tower;
        if (_tower.GetTier() >= TowerCostData.MAXTier)
        {
            _upgradeButton.enabled = false;
            _upgradeButton.GetComponentInChildren<Text>().text = "Maxed!";
            _tracker.GreenCount.text = "0";
            _tracker.BlueCount.text = "0";
            _tracker.PurpleCount.text = "0";
            _tracker.RedCount.text = "0";
        }
        else
        {
            SetUpgradeCost();
        }
    }

    private void SetUpgradeCost()
    {

        if (_tower.towerType == TowerType.Heavy)
        {
            _tracker.GreenCount.text = TowerCostData.HeavyTower[_tower.GetTier() + 1][0].ToString();
            _tracker.BlueCount.text = TowerCostData.HeavyTower[_tower.GetTier() + 1][1].ToString();
            _tracker.PurpleCount.text = TowerCostData.HeavyTower[_tower.GetTier() + 1][2].ToString();
            _tracker.RedCount.text = TowerCostData.HeavyTower[_tower.GetTier() + 1][3].ToString();
        }
        else if (_tower.towerType == TowerType.Normal)
        {
            _tracker.GreenCount.text = TowerCostData.MediumTower[_tower.GetTier() + 1][0].ToString();
            _tracker.BlueCount.text = TowerCostData.MediumTower[_tower.GetTier() + 1][1].ToString();
            _tracker.PurpleCount.text = TowerCostData.MediumTower[_tower.GetTier() + 1][2].ToString();
            _tracker.RedCount.text = TowerCostData.MediumTower[_tower.GetTier() + 1][3].ToString();
        }
        else if (_tower.towerType == TowerType.Sniper)
        {
            _tracker.GreenCount.text = TowerCostData.LightTower[_tower.GetTier() + 1][0].ToString();
            _tracker.BlueCount.text = TowerCostData.LightTower[_tower.GetTier() + 1][1].ToString();
            _tracker.PurpleCount.text = TowerCostData.LightTower[_tower.GetTier() + 1][2].ToString();
            _tracker.RedCount.text = TowerCostData.LightTower[_tower.GetTier() + 1][3].ToString();
        }
    }
}
