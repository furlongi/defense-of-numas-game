using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowWaypoints : MonoBehaviour
{

    private List<Waypoint> _waypointList;
    private int _currentWaypointIndex = 0;
    
    private TowerEnemy _towerEnemy;
    private SpriteRenderer _sprite;

    void Start()
    {
        _waypointList = GameObject.Find("Waypoint List").GetComponent<WaypointList>().GetWaypointList();
        _towerEnemy = GetComponent<TowerEnemy>();
        _sprite = GetComponent<SpriteRenderer>();
        GetComponent<Animator>().speed = _towerEnemy.speed;
        
        if (_towerEnemy == null)
        {
            Debug.Log("No Enemy selected.");
        }
        else if (_waypointList == null)
        {
            Debug.Log("No Waypoint List found in scene.");
        }
    }

    private void FixedUpdate()
    {
        if (!_towerEnemy.IsDead())
        {
            Vector2 pos = transform.position;
            Vector2 distanceFromWaypoint = _waypointList[_currentWaypointIndex].GetPosition() - pos;
            distanceFromWaypoint.Normalize();
            Vector2 movement = pos + (_towerEnemy.speed * Time.deltaTime * distanceFromWaypoint);
            if (distanceFromWaypoint.x > 0)
            {
                _sprite.flipX = true;
            }

            if (distanceFromWaypoint.x < 0)
            {
                _sprite.flipX = false;
            }

            _towerEnemy.Move(movement);
            _towerEnemy.DistanceTraveled += Vector2.Distance(pos, movement);
        }
        else
        {
            _towerEnemy.speed = 0f;
            _towerEnemy.SetVelocity(Vector2.zero);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Waypoint") && other.GetComponent<Waypoint>() == _waypointList[_currentWaypointIndex])
        {
            if (_currentWaypointIndex < _waypointList.Count - 1)
            {
                _currentWaypointIndex++;
            }
            else if (_currentWaypointIndex == _waypointList.Count - 1)
            {
                Destroy(gameObject);
                _towerEnemy.Round.EnemiesAlive.Remove(_towerEnemy);
                _towerEnemy.Round.Wave.DecrementCurrentPopulation();
            }   
        }
    }
}

