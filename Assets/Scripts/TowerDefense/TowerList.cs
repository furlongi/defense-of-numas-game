using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerList : MonoBehaviour
{
    public List<BaseTower> towerList;

    public void Awake()
    {
        towerList = new List<BaseTower>();
    }

    public void AddTower(BaseTower tower)
    {
        towerList.Add(tower);
    }


}
