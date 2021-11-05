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

    // Start is called before the first frame update
    void Start()
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
    
    // Update is called once per frame
    void Update()
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

    // FixedUpdate update interval synced with physics update interval
    private void FixedUpdate()
    {
        if (_isInsideEnemy)
        {
            _rb.MovePosition((Vector2)transform.position + (enemy.speed * Time.deltaTime * _avoidEnemyDirection));
        }
        if (_isChasing && !_isNearPlayer)
        {
            _rb.MovePosition((Vector2)transform.position + (enemy.speed * Time.deltaTime * _movement));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
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

    public bool IsNear()
    {
        return _isNearPlayer;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (Vector3.Distance(other.transform.position, transform.position) < 1.2)
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
        if (enemy.detect < 0)
        {
            _isChasing = true;
            _effector.forceMagnitude = _setEffectorMagn;
            ContinueAnimation();
            return;
        }

        if (_isChasing && enemy.vision > 0 && distance > enemy.vision)
        {
            _isChasing = false;
            PauseAnimation();
            return;
        }

        if (distance < enemy.detect)
        {
            _isChasing = true;
            _effector.forceMagnitude = _setEffectorMagn;
            ContinueAnimation();
        }
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
