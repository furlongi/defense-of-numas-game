using System;
using UnityEngine;

public class TowerRadius : MonoBehaviour
{
    private Transform _objectTransform;

    void Start()
    {
        _objectTransform = gameObject.transform;
        _objectTransform.position = _objectTransform.parent.position;
    }
}
