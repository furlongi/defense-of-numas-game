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
    private PointEffector2D _effector;
    
    private bool _isChasing = false;
    private bool _isNearPlayer = false;
    private bool _isInsideEnemy = false;

    private Vector2 _avoidEnemyDirection = new Vector2();
    private float _setAniSpeed = 0;
    private float _setEffectorMagn = 0;


    private void Start()
    {
        playerLoc = GameObject.FindWithTag("Player").transform;
        
        if (enemy == null) {
            Debug.Log("No Enemy is selected in editor.");
        }
        
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _effector = GetComponent<PointEffector2D>();
        _setAniSpeed = _animator.speed;
        _animator.speed = 0;
        _setEffectorMagn = _effector.forceMagnitude;
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
        if (_isInsideEnemy) // Move away from a fellow enemy to avoid stacking
        {
            _rb.MovePosition((Vector2)transform.position + (enemy.speed * Time.deltaTime * _avoidEnemyDirection));
        }
        if (_isChasing && !_isNearPlayer) // If near player, then do not overlay inside the player
        {
            _rb.MovePosition((Vector2)transform.position + (enemy.speed * Time.deltaTime * _movement));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            /* If this enemy is inside another enemy, it will attempt to move
             * out of the way.
             *
             * If the OTHER enemy is chasing while near the player (stopped in front),
             * then move a different direction while still going towards the player.
             * This will allow all enemies to circle around a stationary player.
             *
             * If THIS is no longer chasing or near the player, but still inside
             * another enemy, then move some random direction. Give these
             * boys some space!
             */
            var enScript = other.gameObject.GetComponent<EnemyChase>();
            _isInsideEnemy = true;
            
            if (enScript._isChasing && enScript._isNearPlayer)
            {
                ContinueAnimation();
                Vector2 thisPos = transform.position;
                Vector2 otherPos = other.gameObject.transform.position;
                var vect = thisPos - otherPos;

                _avoidEnemyDirection = Math.Abs(_movement.x) > Math.Abs(_movement.y)
                    ? new Vector2(0, vect.y)
                    : new Vector2(vect.x, 0);

                _avoidEnemyDirection.Normalize();
            }
            else if (!_isChasing && !_isNearPlayer)
            {
                var r = new System.Random();
                _avoidEnemyDirection = new Vector2(r.Next(-10, 10), r.Next(-10,10));
                _avoidEnemyDirection.Normalize();
                ContinueAnimation();
            }
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        /* Checks if this enemy is inside a player or not to stop a certain distance */
        if (other.gameObject.name == "Player")
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
        if (other.gameObject.name == "Player")
        {
            _isNearPlayer = false;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            _isInsideEnemy = false;
            var enScript = other.gameObject.GetComponent<EnemyChase>();
            if (!enScript._isChasing && !enScript._isNearPlayer)
            {
                _effector.forceMagnitude = 0;
                _avoidEnemyDirection = new Vector2(0, 0);
                PauseAnimation();
            }
            PauseAnimation();
        }
    }

    private void CheckShouldChase(float distance)
    {
        // Infinite detection; Always chase
        if (enemy.detect < 0)
        {
            _isChasing = true;
            _effector.forceMagnitude = _setEffectorMagn;
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
            _effector.forceMagnitude = _setEffectorMagn;
            ContinueAnimation();
        }
    }
    
    public bool IsNear()
    {
        return _isNearPlayer;
    }

    private void PauseAnimation()
    {
        _animator.speed = 0;
    }
    
    private void ContinueAnimation()
    {
        _animator.speed = _setAniSpeed;
    }
}
