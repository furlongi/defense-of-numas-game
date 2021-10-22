using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{

    public Rigidbody2D _rb;
    public TowerEnemy enemy;

    private List<Waypoint> waypointList;
    private int _currentWaypointIndex;

    private void Awake()
    {
        waypointList = GameObject.Find("Waypoint List").GetComponent<WaypointList>().GetWaypointList();
        _currentWaypointIndex = 0;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
        if (enemy == null)
        {
            Debug.Log("No Enemy selected.");
        }
        else if (waypointList == null)
        {
            Debug.Log("No Waypoint List found in scene.");
        }
        else if (_rb == null)
        {
            Debug.Log("No RigidBody2D selected.");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector2 movement = waypointList[_currentWaypointIndex].GetPosition() - (Vector2)transform.position;
        movement.Normalize();
        _rb.MovePosition((Vector2)transform.position + (enemy.speed * Time.deltaTime * movement));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_currentWaypointIndex < waypointList.Count - 1 && other.gameObject.CompareTag("Waypoint"))
        {
            print("Updated current waypoint");

            _currentWaypointIndex++;
        }
    }
}

