using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerShopItem : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler
{
    private TowerShop _towerShop;
    private RectTransform _rectTransform;
    public Canvas canvas;
    
    public BaseTower towerPrefab;

    [NonSerialized] public InventoryTracker ItemCost;
    
    private bool _cancelTowerItemOnDrop = false;
    
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _towerShop = GetComponentInParent<TowerShop>();
        ItemCost = GetComponentInChildren<InventoryTracker>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject newShopItem = Instantiate(gameObject);
        newShopItem.GetComponent<RectTransform>().SetParent(_towerShop.GetComponent<RectTransform>(), false);
        Destroy(GetComponentInChildren<InventoryTracker>().gameObject);

        GameObject prefab = Instantiate(towerPrefab, transform.position, Quaternion.identity).gameObject;
        GameObject radius = prefab.transform.GetChild(0).gameObject;
        radius.transform.parent = gameObject.transform;
        radius.GetComponent<TowerRadius>().IsBeingDragged = true;
        Destroy(prefab);

        Vector2 pos = _rectTransform.position;
        _rectTransform.SetParent(canvas.transform, false);
        _rectTransform.position = pos;
        _towerShop.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition +=  eventData.delta / canvas.scaleFactor;
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        _towerShop.gameObject.SetActive(true);

        if (_cancelTowerItemOnDrop)
        {
            Destroy(gameObject);
        }
        else
        {
            if (_towerShop.PurchaseTower(ItemCost))
            {
                BaseTower newTower = Instantiate(towerPrefab);
                newTower.gameObject.transform.position = _rectTransform.position;                
            }
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TowerItemCancel"))
        {
            _cancelTowerItemOnDrop = true;
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("TowerItemCancel"))
        {
            _cancelTowerItemOnDrop = false;
        }
    }
}
