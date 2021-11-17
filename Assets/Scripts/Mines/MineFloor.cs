using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineFloor
{
    public Vector3 spawnPoint;
    public EnemySpawner[] spawnPoints;
    private int _enemiesSpawned;
    private List<GameObject> _activeEnemies;
    
    
    public int RemainingEnemies()
    {
        return _enemiesSpawned;
    }

    public void DecrementEnemies()
    {
        _enemiesSpawned -= 1;
    }

    public void Load(MineManager manager)
    {
        ClearFloor();
        Player player = manager.player;
        player.transform.position = spawnPoint;

        List<SpawnRates.EnemyType> enemyList =
            SpawnRates.CalculateProbability(manager.Difficulty(), manager.traveresedFloors, spawnPoints.Length);
        
        SpawnEnemies(enemyList, manager);
    }

    private void SpawnEnemies(List<SpawnRates.EnemyType> enemyList, MineManager manager)
    {
        for (int i = 0; i < enemyList.Count; ++i)
        {
            EnemySpawner spawn = spawnPoints[i];
            _activeEnemies.Add(manager.SpawnEnemy(enemyList[i], spawn.transform));
        }
    }

    public void ClearFloor()
    {
        foreach (var activeEnemy in _activeEnemies)
        {
            if (activeEnemy != null)
            {
                MineManager.DestroyAnEnemy(activeEnemy);
            }
        }
        _activeEnemies.Clear();
    }
    
}
