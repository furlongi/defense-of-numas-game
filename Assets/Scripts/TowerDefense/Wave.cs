using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TowerDefense;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Wave : MonoBehaviour
{
    private List<Round> rounds;
    private Vector2 spawnLocation;
    public int waveNumber;
    private int currentRound = 0;
    private Text _currentRoundCounter;
    
    public GameObject RoundCounter;
    public GameObject GreenTowerEnemy;
    public GameObject BlueTowerEnemy;
    public GameObject RedTowerEnemy;
    public GameObject EnemySpawner;
    
    void Start()
    {
        _currentRoundCounter = RoundCounter.GetComponent<Text>();
        spawnLocation = EnemySpawner.transform.position;
        rounds = new List<Round>();
        SetWaveRounds();
        _currentRoundCounter.text = "Round 1 / " + rounds.Count;

    }

    void FixedUpdate()
    {
        if (currentRound < rounds.Count && !rounds[currentRound].isGoing)
        {
            rounds[currentRound].isGoing = true;
            StartCoroutine(ExecuteRound(currentRound));
        }
    }

    IEnumerator ExecuteRound(int roundIndex)
    {
        for (int i = 0; i < rounds[roundIndex].Enemies.Count; i++)
        {
            EnemyCluster cluster = rounds[roundIndex].Enemies[i];
            GameObject newTowerEnemy;
            for (int j = 0; j < cluster.Count; j++)
            {
                if (cluster.EnemyID == EnemyCluster.GREEN)
                {
                    newTowerEnemy = Instantiate(GreenTowerEnemy, spawnLocation, Quaternion.identity);
                }
                else if (cluster.EnemyID == EnemyCluster.BLUE)
                {
                    newTowerEnemy = Instantiate(BlueTowerEnemy, spawnLocation, Quaternion.identity);
                }
                else
                {
                    newTowerEnemy = Instantiate(RedTowerEnemy, spawnLocation, Quaternion.identity);
                }

                newTowerEnemy.GetComponent<TowerEnemy>().round = rounds[currentRound];
                
                
                
                // Time between enemies spawning in a cluster
                yield return new WaitForSeconds(0.5f);
            }
            // Time between spawning a new cluster after spawning the last enemy in the previous cluster
            yield return new WaitForSeconds(2f);
        }
        while (rounds[currentRound].EnemiesAlive.Count > 0)
        {
            yield return null;
        }
        Debug.Log("Starting Next Round in 5 Seconds");
        yield return new WaitForSeconds(3f);;
        rounds[roundIndex].isGoing = false;
        currentRound++;
        if (currentRound < rounds.Count)
        {
            _currentRoundCounter.text = "Round " + (currentRound + 1) + " / " + rounds.Count;
        }
        else
        {
            _currentRoundCounter.text = "Wave Complete!";
        }

    }
    private void SetWaveRounds()
    {
        for (int i = 0; i < waveNumber * 10; i++) // Wave 1 will have 10 rounds, 2 will have 20, 3 30 rounds
        {
            Round round = new Round();
            round.Enemies.Add(new EnemyCluster(EnemyCluster.GREEN, 3*waveNumber));
            rounds.Add(round);
        }
    }
    
}
