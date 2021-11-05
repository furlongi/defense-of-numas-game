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

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition((Vector2)transform.position + bulletForce * Time.deltaTime * direction);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<BaseEnemy>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
