using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.1f;
    private Vector3 input;
    public GameObject enemy;
    public Animator animator;
    public SpriteRenderer _sprite;


    private void Start()
    {
        // _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.magnitude > 1)
        {
            
        }

        if (input.magnitude != 0)
        {
            animator.SetBool("isRunning", true);
            if (input.x > 0)
            {
                _sprite.flipX = false;
            } 
            else if (input.x < 0)
            {
                _sprite.flipX = true;
            }
            input.Normalize();
            transform.position += speed * Time.deltaTime * input;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

    }

    void Update() {
        // For Debug spawning enemies. Delete later
        if (Input.GetKeyDown("r")) {
            System.Random r = new System.Random();
            GameObject b = Instantiate(enemy, transform.position, transform.rotation);
            b.GetComponent<BaseEnemy>().SetAttack((float)r.Next(1, 10));
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        // if(other.gameObject.tag=="bullet")
        //     Destroy(gameObject);    
    }

}
