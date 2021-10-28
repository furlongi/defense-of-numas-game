using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.1f;
    private Vector3 input;
    public GameObject enemy;


    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.magnitude > 1) {
            input.Normalize();
        }

        transform.position += input * speed * Time.deltaTime;
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
