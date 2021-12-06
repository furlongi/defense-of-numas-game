using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Wave : MonoBehaviour
{
    private List<Round> rounds = new List<Round>();
    private Vector2 _spawnLocation;
    private int _currentRound = 0;
    private EventManager _eventManager;
    private WaveData _waveData;
    
    void Start()
    {
        _eventManager = gameObject.GetComponent<EventManager>();
        _waveData = _eventManager.transform.GetChild(0).GetComponent<WaveData>();
        SetWaveRounds();
        _spawnLocation = _eventManager.enemySpawner.transform.position;
        _eventManager.RoundCounter.text = "Round 1 / " + rounds.Count;
    }

    void FixedUpdate()
    {
        if (_currentRound < rounds.Count && !rounds[_currentRound].IsGoing)
        {
            rounds[_currentRound].IsGoing = true;
            StartCoroutine(ExecuteRound(_currentRound));
        }
    }

    IEnumerator ExecuteRound(int roundIndex)
    {
        //(rounds[roundIndex].Enemies.Count);
        List<EnemyTypes.EnemyType> roundEnemies = rounds[roundIndex].Enemies;
        for (int i = 0; i < roundEnemies.Count; i++)
        {
            if (roundEnemies[i] == EnemyTypes.EnemyType.Green)
            {
                GameObject newTowerEnemy = Instantiate(_eventManager.greenTowerEnemy, _spawnLocation, Quaternion.identity);
                newTowerEnemy.GetComponent<TowerEnemy>().Round = rounds[_currentRound];
                yield return new WaitForSeconds(0.2f);
            }
            else if (roundEnemies[i] == EnemyTypes.EnemyType.Blue)
            {
                GameObject newTowerEnemy = Instantiate(_eventManager.blueTowerEnemy, _spawnLocation, Quaternion.identity);
                newTowerEnemy.GetComponent<TowerEnemy>().Round = rounds[_currentRound];
                yield return new WaitForSeconds(0.5f);
            }
            else if (roundEnemies[i] == EnemyTypes.EnemyType.Purple)
            {
                GameObject newTowerEnemy = Instantiate(_eventManager.purpleTowerEnemy, _spawnLocation, Quaternion.identity);
                newTowerEnemy.GetComponent<TowerEnemy>().Round = rounds[_currentRound];
                yield return new WaitForSeconds(0.7f);
            }
            else
            {
                GameObject newTowerEnemy = Instantiate(_eventManager.redTowerEnemy, _spawnLocation, Quaternion.identity);
                newTowerEnemy.GetComponent<TowerEnemy>().Round = rounds[_currentRound];
                yield return new WaitForSeconds(1f);
            }
        }
        
        while (rounds[_currentRound].EnemiesAlive.Count > 0)
        {
            yield return null;
        }
        //Debug.Log("Starting Next Round in 5 Seconds");
        yield return new WaitForSeconds(3f);;
        rounds[roundIndex].IsGoing = false;
        _currentRound++;
        if (_currentRound < rounds.Count)
        {
            _eventManager.RoundCounter.text = "Round " + (_currentRound + 1) + " / " + rounds.Count;
        }
        else
        {
            _eventManager.IsOver = true;
            _eventManager.waveNumber++;
        }

    }
    private void SetWaveRounds()
    {
        List<WaveData.RoundData> enemies = new List<WaveData.RoundData>();
        if (_eventManager.waveNumber == 1)
        {
            enemies = _waveData.wave1;
        }
        else if (_eventManager.waveNumber == 2)
        {
            enemies = _waveData.wave2;
        }
        else if (_eventManager.waveNumber == 3)
        {
            enemies = _waveData.wave3;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            Round round = gameObject.AddComponent<Round>();
            for (int j = 0; j < enemies[i].roundData.Count; j++)
            {
                for (int k = 0; k < enemies[i].roundData[j].number; k++)
                {
                    round.Enemies.Add(enemies[i].roundData[j].enemyType);
                }
            }
        }

        rounds = GetComponents<Round>().ToList();
    }

    public void DecrementCurrentPopulation()
    {
        _eventManager.currentPopulation--;
        _eventManager.CurrentPopulationModified = true;
    }
}
