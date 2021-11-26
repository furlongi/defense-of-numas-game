using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
If needed, this code can be changed to get the Enemy class using
GetComponent<BaseEnemy>()
if this script is attached as a component.

However, current implementation will assume you link the Enemy object
using the editor with the drag and drop method. Read BaseEnemy.cs for
more info. You do this by, for example BaseEnemy, dragging the
attached BaseEnemy script THAT IS INSIDE THE COMPONENT LIST INSPECTOR 
into the 'enemy' section. This is so that it correctly references
the values of the prefab's designated BaseEnemy script, in case
some values are changed in the inspector.
*/


public class EnemyChase : MonoBehaviour
{
    public BaseEnemy enemy;
    public Transform playerLoc;
    
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private Animator _animator;

    private bool _isChasing = false;
    private float _setAniSpeed = 0;
    private bool _isNearPlayer = false;


    private void Start()
    {
        playerLoc = GameObject.FindWithTag("Player").transform;
        
        if (enemy == null) {
            Debug.Log("No Enemy is selected in editor.");
        }
        
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _setAniSpeed = _animator.speed;
        _animator.speed = 0;
    }
    
    private void Update()
    {
        var playerPos = playerLoc.position;
        var curPos = transform.position;
        var distance = Vector2.Distance(playerPos, curPos);

        CheckShouldChase(distance);
        
        if (_isChasing)
        {
            _movement = playerPos - curPos;
            _movement.Normalize();
            if (_movement.x > 0)
            {
                _sprite.flipX = true;
            } 
            else if (_movement.x < 0)
            {
                _sprite.flipX = false;
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (_isChasing && !_isNearPlayer) // If near player, then do not overlay inside the player
        {
            _rb.velocity = _movement * enemy.speed;
        }
        else
        {
            _rb.velocity = new Vector2(0, 0);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        /* Checks if this enemy is inside a player or not to stop a certain distance */
        if (other.gameObject.CompareTag("Player"))
        {
            if (Vector3.Distance(other.transform.position, transform.position) < enemy.stopRange)
            {
                _isNearPlayer = true;
            }
            else
            {
                _isNearPlayer = false;
            }
        }
    }
    
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isNearPlayer = false;
        }
    }


    public Vector2 GetMovement()
    {
        return _movement;
    }
    
    
    private void CheckShouldChase(float distance)
    {
        // Infinite detection; Always chase
        if (enemy.detect < 0)
        {
            _isChasing = true;
            ContinueAnimation();
            return;
        }

        // If vision is not infinite and player is out of enemy's vision, then stop chasing
        if (_isChasing && enemy.vision > 0 && distance > enemy.vision)
        {
            _isChasing = false;
            PauseAnimation();
            return;
        }

        // If within detection, start chase
        if (distance < enemy.detect)
        {
            _isChasing = true;
            // _effector.forceMagnitude = _setEffectorMagn;
            ContinueAnimation();
        }
    }

    private void PauseAnimation()
    {
        if (!enemy.IsDead())
        {
            _animator.speed = 0;
        }
    }
    
    private void ContinueAnimation()
    {
        _animator.speed = _setAniSpeed;
    }

    public bool IsNear()
    {
        return _isNearPlayer;
    }
    
}
