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
    
    private int _upgradeCostGreen;
    private int _upgradeCostBlue;
    private int _upgradeCostPurple;
    private int _upgradeCostRed;
    private bool _canPurchase;

    private Text _isMaxed;
    void Start()
    {
        _upgradeButton = GetComponentInChildren<Button>();
        _upgradeButton.onClick.AddListener(PurchaseUpgrade);
        _isMaxed = transform.GetChild(0).GetComponent<Text>();
        _isMaxed.gameObject.SetActive(false);
        _tracker = GetComponentInChildren<InventoryTracker>();
        gameObject.SetActive(false);
    }

    private void PurchaseUpgrade()
    {
        playerInventory.GemTransaction(new int[] {_upgradeCostGreen, _upgradeCostBlue, _upgradeCostPurple, _upgradeCostRed});
        _tower.UpgradeTower();
        RefreshComponent();
    }

    public void SetTower(BaseTower tower)
    {
        _tower = tower;
        RefreshComponent();
    }
    public void RefreshComponent() {
        
    if (_tower.GetTier() >= TowerCostData.MAXTier)
        {
            _upgradeButton.gameObject.SetActive(false);
            _tracker.gameObject.SetActive(false);
            _isMaxed.gameObject.SetActive(true);
        }
        else
        {
            _upgradeButton.gameObject.SetActive(true);
            _tracker.gameObject.SetActive(true);
            _isMaxed.gameObject.SetActive(false);
            Text text = _upgradeButton.GetComponentInChildren<Text>();
            SetUpgradeCost();
            if (!_canPurchase)
            {
                _upgradeButton.interactable = false;
                text.text = "Insufficient Funds";
                text.fontStyle = FontStyle.Bold;
            }
            else
            {
                _upgradeButton.interactable = true;
                text.text = "Upgrade Tower";
                text.fontStyle = FontStyle.Normal;
            }
        }
    }

    private void SetUpgradeCost()
    {

        if (_tower.towerType == TowerType.Heavy)
        {
            _upgradeCostGreen = TowerCostData.HeavyTower[_tower.GetTier() + 1][0];
            _upgradeCostBlue = TowerCostData.HeavyTower[_tower.GetTier() + 1][1];
            _upgradeCostPurple = TowerCostData.HeavyTower[_tower.GetTier() + 1][2];
            _upgradeCostRed = TowerCostData.HeavyTower[_tower.GetTier() + 1][3];
        }
        else if (_tower.towerType == TowerType.Normal)
        {
            _upgradeCostGreen = TowerCostData.MediumTower[_tower.GetTier() + 1][0];
            _upgradeCostBlue = TowerCostData.MediumTower[_tower.GetTier() + 1][1];
            _upgradeCostPurple = TowerCostData.MediumTower[_tower.GetTier() + 1][2];
            _upgradeCostRed = TowerCostData.MediumTower[_tower.GetTier() + 1][3];
        }
        else if (_tower.towerType == TowerType.Sniper)
        {
            _upgradeCostGreen = TowerCostData.LightTower[_tower.GetTier() + 1][0];
            _upgradeCostBlue = TowerCostData.LightTower[_tower.GetTier() + 1][1];
            _upgradeCostPurple = TowerCostData.LightTower[_tower.GetTier() + 1][2];
            _upgradeCostRed = TowerCostData.LightTower[_tower.GetTier() + 1][3];
        }

        _tracker.GreenCount.text = _upgradeCostGreen.ToString();
        _tracker.BlueCount.text = _upgradeCostBlue.ToString();
        _tracker.PurpleCount.text = _upgradeCostPurple.ToString();
        _tracker.RedCount.text = _upgradeCostRed.ToString();

        _canPurchase = _upgradeCostGreen <= playerInventory.greenGem && _upgradeCostBlue <= playerInventory.blueGem &&
                       _upgradeCostPurple <= playerInventory.purpleGem && _upgradeCostRed <= playerInventory.redGem;
    }
}
