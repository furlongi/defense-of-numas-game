using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class CancelPopup : MonoBehaviour
{

    // public GameObject towerShop;
    // [NonSerialized] public List<Renderer> TowerRadiuses = new List<Renderer>();
    //
    // public void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         RaycastHit2D hit = Physics2D.Raycast(pos, -Vector2.up);
    //         
    //         if (hit.collider != null)
    //         {
    //                
    //             Debug.Log(hit.transform);
    //             if (hit.transform.name == "Popup Closer")
    //             {
    //                 Debug.Log("My object is clicked by mouse");
    //                 CancelPopups();
    //             }
    //         }
    //     }
    // }
    // // public void OnPointerDown(PointerEventData eventData)
    // // {
    // //     Debug.Log("Clicked");
    // //     CancelPopups();
    // // }
    //
    // public void CancelPopups()
    // {
    //     Debug.Log("Canceling popups...");
    //     towerShop.gameObject.SetActive(false);
    //     for (int i = 0; i < TowerRadiuses.Count; i++)
    //     {
    //         TowerRadiuses[i].enabled = false;
    //     }
    // }
}
