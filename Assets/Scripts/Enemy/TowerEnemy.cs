using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemy : BaseEnemy
{
    [NonSerialized] public float distanceTraveled;
    [NonSerialized] public Rigidbody2D rigidBody;
    public Round round;
    private List<BaseTower> targetedTowers;
    
    void Start()
    {
        round.EnemiesAlive.Add(this);
        base.Start();
        targetedTowers = new List<BaseTower>();
        rigidBody = GetComponent<Rigidbody2D>();        
        distanceTraveled = 0;
    }
    
    void Update()
    {
        if (IsDead())
        {
            for (int i = 0; i < targetedTowers.Count; i++)
            {
                targetedTowers[i].targets.Remove(this);
            }

            if (round.EnemiesAlive.Contains(this))
            {
                round.EnemiesAlive.Remove(this);
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
