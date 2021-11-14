using System;
using System.Collections.Generic;
using TowerDefense;
using UnityEngine;

public class Round : MonoBehaviour
{
    public List<EnemyCluster> Enemies;
    public List<TowerEnemy> EnemiesAlive { get; set; } = new List<TowerEnemy>();
    
    public bool isGoing = false;

    public Round(List<EnemyCluster> enemies)
    {
        Enemies = enemies;
    }

    public Round()
    {
        Enemies = new List<EnemyCluster>();
    }
    
    private void FixedUpdate()
    {
        if (EnemiesAlive.Count > 0)
        {
            List<int> enemiesToRemove = new List<int>();
            for (int i = 0; i < EnemiesAlive.Count; i++)
            {
                if (EnemiesAlive[i].GetComponent<TowerEnemy>().IsDead())
                {
                    enemiesToRemove.Add(i);
                }
            }

            for (int i = 0; i < enemiesToRemove.Count; i++)
            {
                EnemiesAlive.RemoveAt(enemiesToRemove[i]);
            }
        }

    }
}
