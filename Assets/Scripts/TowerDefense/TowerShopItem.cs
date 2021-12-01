using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerShopItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler
{
    private TowerShop _towerShop;
    private RectTransform _rectTransform;
    public Canvas canvas;
    private SpriteRenderer _radiusSprite;
    private Color _spriteOriginalColor;
    public BaseTower towerPrefab;

    [NonSerialized] public InventoryTracker ItemCost;
    
    private bool _isOverTrack = false;
    private bool _isOverCancelButton = false;
    private bool _isOverOtherTower = false;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _towerShop = GetComponentInParent<TowerShop>();
        ItemCost = GetComponentInChildren<InventoryTracker>();
        _spriteOriginalColor = towerPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject newShopItem = Instantiate(gameObject);
        newShopItem.GetComponent<RectTransform>().SetParent(_towerShop.GetComponent<RectTransform>(), false);
        Destroy(GetComponentInChildren<InventoryTracker>().gameObject);
        
        GameObject prefab = Instantiate(towerPrefab, transform.position, Quaternion.identity).gameObject;
        GameObject radius = prefab.transform.GetChild(0).gameObject;
        _radiusSprite = radius.GetComponent<SpriteRenderer>();
        radius.transform.parent = gameObject.transform;
        Destroy(prefab);
        _towerShop.gameObject.SetActive(false);

        Vector2 pos = _rectTransform.position;
        _rectTransform.SetParent(canvas.transform, false);
        _rectTransform.position = pos;

        _towerShop.cancelItemDragging.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition +=  eventData.delta / canvas.scaleFactor;
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        _towerShop.gameObject.SetActive(true);
        if (_isOverTrack || _isOverCancelButton || _isOverOtherTower)
        {
            _towerShop.cancelItemDragging.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            if (_towerShop.PurchaseTower(ItemCost))
            {
                Vector3 startingPos = _rectTransform.position;
                startingPos.z = towerPrefab.transform.position.z;
                BaseTower newTower = Instantiate(towerPrefab, startingPos, Quaternion.identity);
                //newTower.transform.GetChild(0).localPosition = new Vector3(0, 0, 1);
            }
            _towerShop.cancelItemDragging.SetActive(false);
            Destroy(gameObject);
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TowerItemCancel"))
        {
            if (_radiusSprite)
            {
                _radiusSprite.color = new Color(1f, 0f, 0f, .2f);
            }
            _isOverCancelButton = true;
        }
        else if (other.CompareTag("Track"))
        {
            if (_radiusSprite)
            {
                _radiusSprite.color = new Color(1f, 0f, 0f, .2f);
            }
            _isOverTrack = true;
        }
        else if (other.CompareTag("TowerHitbox"))
        {
            if (_radiusSprite)
            {
                _radiusSprite.color = new Color(1f, 0f, 0f, .2f);
            }
            _isOverOtherTower = true;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("TowerItemCancel"))
        {
            if (_radiusSprite)
            {
                _radiusSprite.color = new Color(1f, 0f, 0f, .2f);
            }
            _isOverCancelButton = true;
        }
        else if (other.CompareTag("Track"))
        {
            if (_radiusSprite)
            {
                _radiusSprite.color = new Color(1f, 0f, 0f, .2f);
            }
            _isOverTrack = true;
        }
        else if (other.CompareTag("TowerHitbox"))
        {
            if (_radiusSprite)
            {
                _radiusSprite.color = new Color(1f, 0f, 0f, .2f);
            }
            _isOverOtherTower = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("TowerItemCancel"))
        {
            _isOverCancelButton = false;
            if (_radiusSprite && !_isOverTrack && !_isOverOtherTower)
            {
                _radiusSprite.color = _spriteOriginalColor;
            }
        }
        else if (other.CompareTag("Track"))
        {
            _isOverTrack = false;
            if (_radiusSprite && !_isOverCancelButton && !_isOverOtherTower)
            {
                _radiusSprite.color = _spriteOriginalColor;
            }
        }
        else if (other.CompareTag("TowerHitbox"))
        {
            _isOverOtherTower = false;
            if (_radiusSprite && !_isOverTrack && !_isOverCancelButton)
            {
                _radiusSprite.color = _spriteOriginalColor;
            }
        }
    }
}
