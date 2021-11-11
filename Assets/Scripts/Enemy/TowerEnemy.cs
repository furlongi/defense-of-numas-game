using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemy : BaseEnemy
{
    [NonSerialized] public float distanceTraveled;
    [NonSerialized] public Rigidbody2D rigidBody;
    private List<BaseTower> targetedTowers;

    [NonSerialized] public Round round;
    
    // Start is called before the first frame update
    void Start()
    {
        targetedTowers = new List<BaseTower>();
        rigidBody = GetComponent<Rigidbody2D>();        
        distanceTraveled = 0;
    }
    
    void Update()
    {
        if (IsDead())
        {
            print("I died");
            for (int i = 0; i < targetedTowers.Count; i++)
            {
                targetedTowers[i].targets.Remove(this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            BaseTower tower = other.gameObject.GetComponentInParent<BaseTower>();
            if (!tower.targets.Contains(this))
            {
                tower.targets.Add(this);
            }
            if (!targetedTowers.Contains(tower))
            {
                targetedTowers.Add(tower);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            BaseTower tower = other.gameObject.GetComponentInParent<BaseTower>();
            if (tower.targets.Contains(this))
            {
                tower.targets.Remove(this);
            }
            if (targetedTowers.Contains(tower))
            {
                targetedTowers.Remove(tower);
            }
        }
    }
}
