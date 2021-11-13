using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{

    private List<Waypoint> waypointList;
    private int _currentWaypointIndex;
    private TowerEnemy towerEnemy;
    private Animator animationSpeed;
    private SpriteRenderer sprite;

    void Start()
    {
        waypointList = GameObject.Find("Waypoint List").GetComponent<WaypointList>().GetWaypointList();
        towerEnemy = GetComponent<TowerEnemy>();
        animationSpeed = GetComponent<Animator>();
        _currentWaypointIndex = 0;
        sprite = GetComponent<SpriteRenderer>();
        
        GetComponent<Animator>().speed = towerEnemy.speed;
        if (towerEnemy == null)
        {
            Debug.Log("No Enemy selected.");
        }
        else if (waypointList == null)
        {
            Debug.Log("No Waypoint List found in scene.");
        }
    }

    private void FixedUpdate()
    {
        if (!towerEnemy.IsDead())
        {
            Vector2 pos = transform.position;
            Vector2 distanceFromWaypoint = waypointList[_currentWaypointIndex].GetPosition() - pos;
            distanceFromWaypoint.Normalize();
            Vector2 movement = pos + (towerEnemy.speed * Time.deltaTime * distanceFromWaypoint);
            if (distanceFromWaypoint.x > 0)
            {
                sprite.flipX = true;
            }

            if (distanceFromWaypoint.x < 0)
            {
                sprite.flipX = false;
            }
            towerEnemy.rigidBody.MovePosition(movement);
            towerEnemy.distanceTraveled += Vector2.Distance(pos, movement);
        }
        else
        {
            towerEnemy.speed = 0f;
            towerEnemy.rigidBody.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Waypoint"))
        {
            if (_currentWaypointIndex < waypointList.Count - 1)
            {
                _currentWaypointIndex++;
            }
            else if (_currentWaypointIndex == waypointList.Count - 1)
            {
                // Need to set IsDead to True so that enemies at the end of the track are removed from the Round's EnemiesAlive List
                towerEnemy.Damage(10000f);
            }   
        }
    }
}

