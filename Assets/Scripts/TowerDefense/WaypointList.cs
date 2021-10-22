using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointList : MonoBehaviour
{
    [SerializeField]
    private List<Waypoint> waypointList;
    
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
        List<Waypoint> waypoints = GetComponentsInChildren<Waypoint>().ToList();
        for (int i = 0; i < waypoints.Count; i++)
        {
            waypointList.Add(waypoints[i]);
        }
    }

    void Update()
    {
        
    }
}
