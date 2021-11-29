using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerShopToggle : MonoBehaviour
{
    public GameObject towerShop;
    
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(ToggleTowerShopUI);
        towerShop.SetActive(false);
    }

    public void ToggleTowerShopUI()
    {
        towerShop.SetActive(true);
    }
}
