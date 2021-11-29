using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TowerDefense;
using UnityEngine;
using UnityEngine.UI;

public class Wave : MonoBehaviour
{
    private List<Round> rounds = new List<Round>();
    private Vector2 _spawnLocation;
    [NonSerialized] public int WaveNumber;
    private int _currentRound = 0;
    [NonSerialized] public Text RoundCounter;
    private EventManager _eventManager;

    void Start()
    {
        _eventManager = gameObject.GetComponent<EventManager>();
        RoundCounter = _eventManager.roundCounter.GetComponent<Text>();
        WaveNumber = _eventManager.waveNumber;
        _spawnLocation = _eventManager.enemySpawner.transform.position;
        SetWaveRounds();
        RoundCounter.text = "Round 1 / " + rounds.Count;
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
        for (int i = 0; i < rounds[roundIndex].Enemies.Count; i++)
        {
            EnemyCluster cluster = rounds[roundIndex].Enemies[i];
            GameObject newTowerEnemy;
            for (int j = 0; j < cluster.Count; j++)
            {
                if (cluster.EnemyID == EnemyCluster.GREEN)
                {
                    newTowerEnemy = Instantiate(_eventManager.greenTowerEnemy, _spawnLocation, Quaternion.identity);
                }
                else if (cluster.EnemyID == EnemyCluster.BLUE)
                {
                    newTowerEnemy = Instantiate(_eventManager.blueTowerEnemy, _spawnLocation, Quaternion.identity);
                }
                else if (cluster.EnemyID == EnemyCluster.PURPLE)
                {
                    newTowerEnemy = Instantiate(_eventManager.purpleTowerEnemy, _spawnLocation, Quaternion.identity);
                }
                else
                {
                    newTowerEnemy = Instantiate(_eventManager.redTowerEnemy, _spawnLocation, Quaternion.identity);
                }

                newTowerEnemy.GetComponent<TowerEnemy>().Round = rounds[_currentRound];
                //print(newTowerEnemy.GetComponent<TowerEnemy>().Round);
                
                // Time between enemies spawning in a cluster
                yield return new WaitForSeconds(0.5f);
            }
            // Time between spawning a new cluster after spawning the last enemy in the previous cluster
            yield return new WaitForSeconds(2f);
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
            RoundCounter.text = "Round " + (_currentRound + 1) + " / " + rounds.Count;
        }
        else
        {
            RoundCounter.text = "Wave Complete!";
        }

    }
    private void SetWaveRounds()
    {
        for (int i = 0; i < 2; i++)
        {
            Round round = gameObject.AddComponent<Round>();
            round.Enemies.Add(new EnemyCluster(EnemyCluster.GREEN, 2));
            round.Wave = this;
            rounds.Add(round);
        }
    }

    public void decrementCurrentPopulation()
    {
        _eventManager.currentPopulation--;
        _eventManager.CurrentPopulationModified = true;
    }
}
