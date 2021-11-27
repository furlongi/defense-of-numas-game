using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;


public class EventManager : MonoBehaviour
{
    public int waveNumber;
    public int totalPopulation;
    public int currentPopulation;
    private bool _isOver;
    
    [NonSerialized] public bool CurrentPopulationModified = false;

    public GameObject greenTowerEnemy;
    public GameObject blueTowerEnemy;
    public GameObject purpleTowerEnemy;
    public GameObject redTowerEnemy;
    public GameObject enemySpawner;
    public GameObject livesCounter;
    public GameObject roundCounter;

    private Text _livesCounter;
    private Wave _wave;

    void Awake()
    {
        _wave = gameObject.AddComponent<Wave>();
        _wave.WaveNumber = waveNumber;
        _wave.RoundCounter = roundCounter.GetComponent<Text>();
        _livesCounter = livesCounter.GetComponent<Text>();
        _livesCounter.text = "Lives: " + currentPopulation + " / " + totalPopulation;
    }

    void FixedUpdate()
    {
        if (CurrentPopulationModified)
        {
            _livesCounter.text = "Lives: " + currentPopulation + " / " + totalPopulation;
            CurrentPopulationModified = false;
            if (currentPopulation <= 0)
            {
                _isOver = true;
            }
        }

        if (_isOver && currentPopulation <= 0)
        {
            roundCounter.GetComponent<Text>().text = "Game Over...";
            EndWave();
        }
    }

    private void EndWave()
    {
        Destroy(_wave);
        List<GameObject> enemiesToDestroy = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        List<Round> roundsToRemove = GetComponents<Round>().ToList();
        
        for (int i = 0; i < enemiesToDestroy.Count; i++)
        {
            Destroy(enemiesToDestroy[i]);
        }
        
        for (int i = 0; i < roundsToRemove.Count; i++)
        {
            Destroy(roundsToRemove[i]);
        }
    }


}
