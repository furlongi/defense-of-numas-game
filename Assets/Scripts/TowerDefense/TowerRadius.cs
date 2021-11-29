using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerRadius : MonoBehaviour, IPointerDownHandler
{
    private Transform _objectTransform;
    private Renderer _renderer;
    private BaseTower tower;
    void Start()
    {
        _objectTransform = gameObject.transform;
        _objectTransform.position = _objectTransform.parent.position;
        _renderer = GetComponent<Renderer>();
        tower = GetComponentInParent<BaseTower>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Disabling renderer");
        if (_renderer.enabled)
        {        
            Debug.Log("Disabling renderer");
            tower.CancelPopup.CancelPopups();
            _renderer.enabled = false;
        }
    }
}
