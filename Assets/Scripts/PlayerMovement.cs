using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector3 input;

    // Update is called once per frame
    void FixedUpdate()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.magnitude > 1) {
            input.Normalize();
        }

        transform.position += input * speed * Time.deltaTime;
    }

}
