using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShopManager : MonoBehaviour
{
    
    private Transform _panelMenu;
    private Transform _items;

    private void Awake()
    {
        _panelMenu = transform.Find("Panel");
        _items = _panelMenu.Find("ItemPanel");

        _panelMenu.gameObject.SetActive(false);
    }


    private void Start()
    {
        CreateMenuOptions();
    }

    private void CreateMenuOptions()
    {
        // Transform menuTransform = Instantiate(_selections, _weaponMenu);
        // RectTransform menuRect = menuTransform.GetComponent<RectTransform>();
        //
        // float menuHeight = 30f;
        // menuRect.anchoredPosition = new Vector2(0, -menuHeight * 0);
        
    }
    
    
    
}
