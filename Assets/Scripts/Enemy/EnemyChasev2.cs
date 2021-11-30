using System;
using UnityEngine;
using Pathfinding;

public class EnemyChasev2 : MonoBehaviour
{

    private BaseEnemy _self;
    private Transform _playerLoc;
    private Path _path;
    private Seeker _seeker;
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private Animator _animator;
    
    private float waypointDistance = 4f;
    private int _curWaypoint;
    private bool _isReachedEnd;
    
    private bool _isChasing = false;
    private float _setAniSpeed = 0;
    private bool _isNearPlayer = false;

    private void Start()
    {
        _playerLoc = GameObject.FindWithTag("Player").transform;
        _self = GetComponent<BaseEnemy>();
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _setAniSpeed = _animator.speed;
        _animator.speed = 0;
        
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }


    private void FixedUpdate()
    {
        CheckShouldChase(Vector2.Distance(_playerLoc.position, transform.position));
        if (_isChasing && _path != null)
        {
            if (_curWaypoint >= _path.vectorPath.Count)
            {
                EndPath();
                return;
            }

            _isReachedEnd = false;
            
            Vector2 direction = ((Vector2)_path.vectorPath[_curWaypoint] - _rb.position).normalized;
            _rb.velocity = direction * 42f * _self.speed * Time.deltaTime;
            SpriteFlip(_rb.velocity.x);

            if (Vector2.Distance(_rb.position, _path.vectorPath[_curWaypoint]) < waypointDistance)
            {
                _curWaypoint++;
            }
        }
        else
        {
            EndPath();
        }
    }



    private void EndPath()
    {
        _path = null;
        _isReachedEnd = true;
    }

    private void UpdatePath()
    {
        if (_isChasing && _seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, _playerLoc.position, OnPathComplete);
        }
    }
    
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _curWaypoint = 1;
        }
    }

    private void CheckShouldChase(float distance)
    {
        // Infinite detection; Always chase
        if (_self.detect < 0)
        {
            _isChasing = true;
            ContinueAnimation();
            return;
        }

        // If vision is not infinite and player is out of enemy's vision, then stop chasing
        if (_isChasing && _self.vision > 0 && distance > _self.vision)
        {
            _isChasing = false;
            PauseAnimation();
            return;
        }

        // If within detection, start chase
        if (distance < _self.detect)
        {
            _isChasing = true;
            // _effector.forceMagnitude = _setEffectorMagn;
            ContinueAnimation();
        }
    }
    
    private void PauseAnimation()
    {
        if (!_self.IsDead())
        {
            _animator.speed = 0;
        }
    }
    
    private void ContinueAnimation()
    {
        _animator.speed = _setAniSpeed;
    }

    private void SpriteFlip(float x)
    {
        if (x > 0)
        {
            _sprite.flipX = true;
        } 
        else if (x < 0)
        {
            _sprite.flipX = false;
        }
    }
}
