using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.1f;
    
    // Assign the appropriate game objects in inspector
    public Animator animator;
    public SpriteRenderer sprite;
    
    
    private void FixedUpdate()
    {
        Vector3 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.magnitude != 0)
        {
            animator.SetBool("isRunning", true);
            if (input.x > 0)
            {
                sprite.flipX = false;
            } 
            else if (input.x < 0)
            {
                sprite.flipX = true;
            }
            input.Normalize();
            transform.position += speed * Time.deltaTime * input;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

    }

}
