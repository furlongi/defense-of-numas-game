using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    public float modifier = 30f;
    
    // Assign the appropriate game objects in inspector
    public Animator animator;

    private SpriteRenderer _sprite;
    private Rigidbody2D _rb;
    private bool _isInteracting = false;
    private Vector2 _noMovementVec;
    private Player _player;


    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _noMovementVec = new Vector2(0, 0);
        _player = GetComponent<Player>();
}

    private void FixedUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (_player.isDead || _isInteracting || (input.x == 0f && input.y == 0f))
        {
            animator.SetBool("isRunning", false);
            _rb.velocity = _noMovementVec;
        }
        else
        {
            _spriteFlip(input.x);
            input.Normalize();
            animator.SetBool("isRunning", true);
            _rb.velocity = input * speed * modifier * Time.deltaTime;
        }
    }

    public void OccupyPlayer()
        // Use if the player will start an interaction to prevent them from moving
    {
        _isInteracting = true;
    }
    
    public void FreePlayer()
        // Frees player from an interaction, letting them move again
    {
        _isInteracting = false;
    }


    private void _spriteFlip(float x)
    {
        if (x > 0)
        {
            _sprite.flipX = false;
        } 
        else if (x < 0)
        {
            _sprite.flipX = true;
        }
    }
}
