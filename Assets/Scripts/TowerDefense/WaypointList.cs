using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointList : MonoBehaviour
{
    [SerializeField]
    private List<Waypoint> waypointList = new List<Waypoint>();

    public void AddToList(Waypoint waypoint)
    {
        waypointList.Add(waypoint);
    }

    public List<Waypoint> GetWaypointList()
    {
        return waypointList;
    }

    void Start()
    {
    
    }

    void Update()
    {
        
    }
}
