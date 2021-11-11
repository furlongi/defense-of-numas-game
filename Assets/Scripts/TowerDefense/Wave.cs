using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TowerDefense;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Wave : MonoBehaviour
{
    private List<Round> rounds;
    private Vector2 spawnLocation;
    public int waveNumber;
    private int currentRound = 0;

    public GameObject GreenTowerEnemy;
    public GameObject BlueTowerEnemy;
    public GameObject RedTowerEnemy;
    public GameObject EnemySpawner;
    void Awake()
    {
        spawnLocation = EnemySpawner.transform.position;
        rounds = new List<Round>();
        SetWaveRounds();
    }

    void FixedUpdate()
    {
        if (!rounds[currentRound].isGoing)
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
            GameObject newTowerEnemy = new GameObject();
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
                else if (cluster.EnemyID == EnemyCluster.RED)
                {
                    newTowerEnemy = Instantiate(RedTowerEnemy, spawnLocation, Quaternion.identity);
                }
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Starting Next Round");
        yield return new WaitForSeconds(5f);;
        rounds[roundIndex].isGoing = false;
        currentRound++;
        yield return null;
    }
    private void SetWaveRounds()
    {
        for (int i = 0; i < waveNumber * 20; i++)
        {
            Round round = new Round();
            round.Enemies.Add(new EnemyCluster(EnemyCluster.GREEN, 1));
            rounds.Add(round);
        }
    }
    
}
