using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // public GameObject hitEffect;
    public Vector2 direction;
    public float bulletForce = 20f;
    public float damage = 1;
    
    private Rigidbody2D _rb;
    private bool _didHit = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition((Vector2)transform.position + bulletForce * Time.deltaTime * direction);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!_didHit && other.gameObject.CompareTag("Enemy"))
        {
            _didHit = true;
            BaseEnemy enemy = other.GetComponentInParent<BaseEnemy>();
            if (!enemy.IsDead())
            {
                enemy.Damage(damage);
                Destroy(gameObject);
            }
            else
            {
                _didHit = false;
            }
        }
        else if (other.gameObject.CompareTag("Wall") && other.gameObject.layer != 4)
        {
            Destroy(gameObject);
        }
    }
}
