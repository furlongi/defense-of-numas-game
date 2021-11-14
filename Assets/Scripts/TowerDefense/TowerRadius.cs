using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRadius : MonoBehaviour
{
    void Start()
    {
        float radiusScale = gameObject.GetComponentInParent<BaseTower>().radiusScale;

        gameObject.transform.localScale = new Vector3(10 * radiusScale, 18 * radiusScale, 1);
        gameObject.transform.position = gameObject.transform.parent.position;

    }

    void Update()
    {
        gameObject.transform.position = gameObject.transform.parent.position;
    }
}
