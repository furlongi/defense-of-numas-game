using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public GameObject waypointObject;
    private Vector3 position;

    // Start is called before the first frame update

    public Vector3 GetPosition()
    {
        return this.position;
    }

    public void UpdatePosition(Vector3 newPosition)
    {
        this.position = newPosition;
    }
    void Start()
    {
        position = waypointObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        this.position = gameObject.transform.localPosition;
    }
}
