using System;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{

    [NonSerialized] public List<EnemyTypes.EnemyType> Enemies = new List<EnemyTypes.EnemyType>();
    [NonSerialized] public List<TowerEnemy> EnemiesAlive = new List<TowerEnemy>();

    [NonSerialized] public bool IsGoing = false;
    [NonSerialized] public Wave Wave;
    
    public void Start()
    {
        Wave = gameObject.GetComponent<Wave>();
    }
}
