using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private Vector2 position;

    // Start is called before the first frame update

    public Vector2 GetPosition()
    {
        return this.position;
    }

    public void UpdatePosition(Vector2 newPosition)
    {
        this.position = newPosition;
    }
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
