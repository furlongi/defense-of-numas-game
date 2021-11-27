using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemy : BaseEnemy
{
    [NonSerialized] public float DistanceTraveled;
    [NonSerialized] public Round Round;
    
    private List<BaseTower> TargetedTowers = new List<BaseTower>();
    private Rigidbody2D RigidBody;
    
    void Start()
    {
        Round.EnemiesAlive.Add(this);
        base.Start();
        RigidBody = GetComponent<Rigidbody2D>();
        DistanceTraveled = 0;
    }
    
    void Update()
    {
        if (IsDead())
        {
            for (int i = 0; i < TargetedTowers.Count; i++)
            {
                TargetedTowers[i].Targets.Remove(this);
            }

            if (Round.EnemiesAlive.Contains(this))
            {
                Round.EnemiesAlive.Remove(this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            BaseTower tower = other.gameObject.GetComponentInParent<BaseTower>();
            if (!tower.Targets.Contains(this))
            {
                tower.Targets.Add(this);
            }
            if (!TargetedTowers.Contains(tower))
            {
                TargetedTowers.Add(tower);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            BaseTower tower = other.gameObject.GetComponentInParent<BaseTower>();
            if (tower.Targets.Contains(this))
            {
                tower.Targets.Remove(this);
            }

            if (TargetedTowers.Contains(tower))
            {
                TargetedTowers.Remove(tower);
            }
        }
    }

    public void SetVelocity(Vector2 velocity)
    {
        RigidBody.velocity = velocity;
    }

    public void Move(Vector2 movement)
    {
        RigidBody.MovePosition(movement);
    }
}
