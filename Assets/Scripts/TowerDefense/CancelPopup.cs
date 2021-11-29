using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class CancelPopup : MonoBehaviour, IPointerDownHandler
{

    public GameObject towerShop;
    [NonSerialized] public List<Renderer> TowerRadiuses = new List<Renderer>();

    public void OnPointerDown(PointerEventData eventData)
    {
        CancelPopups();
    }
    
    public void CancelPopups()
    {
        Debug.Log("Canceling popups...");
        towerShop.gameObject.SetActive(false);
        for (int i = 0; i < TowerRadiuses.Count; i++)
        {
            TowerRadiuses[i].enabled = false;
        }
    }



}
