using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerShopToggle : MonoBehaviour
{
    private bool _isActive = false;
    public GameObject towerShop;
    
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(ToggleTowerShopUI);
        towerShop.SetActive(false);
    }

    public void ToggleTowerShopUI()
    {
        _isActive = !_isActive;
        towerShop.SetActive(_isActive);
    }
}
