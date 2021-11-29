using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TowerTrack : MonoBehaviour, IPointerDownHandler
{
    private CancelPopup _cancelPopup;
    // Start is called before the first frame update
    void Start()
    {
        _cancelPopup = GameObject.Find("Popup Closer").GetComponent<CancelPopup>();
    }

    // Update is called once per frame
    public void OnPointerDown(PointerEventData eventData)
    {
        _cancelPopup.CancelPopups();
    }
}
