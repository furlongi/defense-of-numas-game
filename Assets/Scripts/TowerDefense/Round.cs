using System.Collections.Generic;
using TowerDefense;
using UnityEngine;

public class Round : MonoBehaviour
{
    public List<EnemyCluster> Enemies { get; set; } = new List<EnemyCluster>();
    public bool isGoing { get; set; } = false;
    
}
