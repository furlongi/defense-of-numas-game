using System;
using UnityEngine;

public class TowerRadius : MonoBehaviour
{
    private Transform _objectTransform;
    
    private bool _hasCollider = true;
    [NonSerialized] public bool IsBeingDragged = false;
    
    void Start()
    {
        _objectTransform = gameObject.transform;
        _objectTransform.position = _objectTransform.parent.position;
    }

    void Update()
    {
        if (IsBeingDragged && _hasCollider)
        {
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            _hasCollider = false;
        }

        else if (!IsBeingDragged && !_hasCollider)
        {
            CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
            collider.isTrigger = true;
        }
    }
}
