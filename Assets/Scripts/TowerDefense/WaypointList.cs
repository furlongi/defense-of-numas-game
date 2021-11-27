using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointList : MonoBehaviour
{
    private List<Waypoint> waypointList = new List<Waypoint>();

    public List<Waypoint> GetWaypointList()
    {
        return waypointList;
    }

    void Start()
    {
        List<Waypoint> waypoints = GetComponentsInChildren<Waypoint>().ToList();
        for (int i = 0; i < waypoints.Count; i++)
        {
            waypointList.Add(waypoints[i]);
        }
    }
}
