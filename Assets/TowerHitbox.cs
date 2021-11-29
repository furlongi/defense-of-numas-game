using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerHitbox : MonoBehaviour, IPointerDownHandler
{
    private BaseTower _tower;
    
    public void Start()
    {
        _tower = GetComponentInParent<BaseTower>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Showing radius");
        _tower.CancelPopup.CancelPopups();
        transform.parent.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
    }
}
