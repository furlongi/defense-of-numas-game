using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 20;
    public static int gems = 0;
    
    void Start()
    {
        
    }
    
    void Update()
    {

    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Game over! (Death not implemented yet)");
        }
    }
}
