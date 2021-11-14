using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private Vector2 position;
    
    public Vector2 GetPosition()
    {
        return this.position;
    }
    
    void Awake()
    {
        Vector3 pos = transform.position;
        gameObject.transform.position = new Vector3(pos.x, pos.y, -100);
        position = (Vector2) pos;
    }
}
