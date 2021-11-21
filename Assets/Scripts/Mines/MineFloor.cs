using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MineFloor : MonoBehaviour
{
    public EnemySpawner[] spawnPoints;
    public int minSpawnSize;
    private int _enemiesSpawned;
    private List<GameObject> _activeEnemies;

    private void Start()
    {
        _activeEnemies = new List<GameObject>();
    }

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
        player.transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);

        List<EnemyTypes.EnemyType> enemyList =
            SpawnRates.CreateEnemyList(manager.Difficulty(), manager.traveresedFloors, spawnPoints.Length, minSpawnSize);

        SpawnEnemies(enemyList, manager);
    }

    private void SpawnEnemies(List<EnemyTypes.EnemyType> enemyList, MineManager manager)
    {
        for (int i = 0; i < enemyList.Count && i < spawnPoints.Length; ++i)
        {
            EnemySpawner spawn = spawnPoints[i];
            _activeEnemies.Add(spawn.SpawnEnemy(enemyList[i], manager, manager.Difficulty(), manager.traveresedFloors));
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
