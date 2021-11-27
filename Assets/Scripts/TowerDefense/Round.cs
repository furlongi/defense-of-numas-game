using System;
using System.Collections.Generic;
using TowerDefense;
using UnityEngine;

public class Round : MonoBehaviour
{
    public List<EnemyCluster> Enemies = new List<EnemyCluster>();
    [NonSerialized] public List<TowerEnemy> EnemiesAlive = new List<TowerEnemy>();

    [NonSerialized] public bool IsGoing = false;
    [NonSerialized] public Wave Wave;
    
    public void Start()
    {
        Wave = gameObject.GetComponent<Wave>();
    }
}
