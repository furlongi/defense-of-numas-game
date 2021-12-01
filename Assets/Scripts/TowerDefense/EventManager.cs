using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;


public class EventManager : MonoBehaviour
{
    
    public int waveNumber;
    public int totalPopulation;
    public int currentPopulation;
    private bool _isOver;
    private BaseTower _currentlySelectedTower;
    
    [NonSerialized] public List<Renderer> TowerRadiuses = new List<Renderer>();
    [NonSerialized] public bool CurrentPopulationModified = false;
    [NonSerialized] public Text RoundCounter;
    [NonSerialized] public Text LivesCounter;
    [NonSerialized] public GameObject TowerDefenseUI;
    [NonSerialized] public GameObject TowerShop;
    [NonSerialized] public Camera Camera;
    
    public GameObject greenTowerEnemy;
    public GameObject blueTowerEnemy;
    public GameObject purpleTowerEnemy;
    public GameObject redTowerEnemy;
    public GameObject enemySpawner;

    public BaseTower NormalTower;
    public BaseTower HeavyTower;
    public BaseTower SniperTower;

    public TowerList towerList;

    private Wave _wave;

    void Awake()
    {
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
        TowerDefenseUI = GameObject.Find("Tower Defense UI");
        RoundCounter = TowerDefenseUI.transform.Find("Round Counter").GetComponent<Text>();
        LivesCounter = TowerDefenseUI.transform.Find("Lives Counter").GetComponent<Text>();
        TowerShop = TowerDefenseUI.transform.Find("Tower Shop").gameObject;

        _wave = gameObject.AddComponent<Wave>();
        waveNumber = PlayerPrefs.GetInt("Wave", 1);
        _wave.WaveNumber = waveNumber;

        LivesCounter.text = "Lives: " + currentPopulation + " / " + totalPopulation;
    }

    private void Start()
    {
        TowerSpawnerFromLoad(SaveSystem.LoadTowerDataList());
    }

    void FixedUpdate()
    {
        if (CurrentPopulationModified)
        {
            LivesCounter.text = "Lives: " + currentPopulation + " / " + totalPopulation;
            CurrentPopulationModified = false;
            if (currentPopulation <= 0 && !_isOver)
            {
                _isOver = true;
            }
        }

        if (_isOver && currentPopulation <= 0 && !RoundCounter.text.Contains("Game Over"))
        {
            RoundCounter.text = "Game Over...";
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

    public void HandleUserClick()
    {
        
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Vector2.zero, 100);
        bool shouldCancelPopups = true;
        
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.CompareTag("TowerHitbox"))
            {
                CancelPopups();
                hits[i].transform.parent.Find("Radius").GetComponent<Renderer>().enabled = true;
                shouldCancelPopups = false;
            }
            else if (hits[i].transform.CompareTag("TowerShop"))
            {
                shouldCancelPopups = false;
            }
        }

        if (shouldCancelPopups)
        {
            CancelPopups();
        }
    }
    
    public void CancelPopups()
    {
        TowerShop.SetActive(false);
        for (int i = 0; i < TowerRadiuses.Count; i++)
        {
            TowerRadiuses[i].enabled = false;
        }
    }

    public void OnMouseDown()
    {
        HandleUserClick();
    }

    private void TowerSpawnerFromLoad(TowerDataList tList)
    {
        if (tList != null)
        {
            if (towerList != null)
            {
                foreach (var towerData in tList.towerList)
                {
                    Vector3 loc = new Vector3(towerData.location[0], towerData.location[1], towerData.location[2]);
                    switch (towerData.type)
                    {
                        case (int)TowerType.Heavy:
                            towerList.AddTower(Instantiate(HeavyTower, loc, Quaternion.identity));
                            break;
                        case (int)TowerType.Normal:
                            towerList.AddTower(Instantiate(NormalTower, loc, Quaternion.identity));
                            break;
                        case (int)TowerType.Sniper:
                            towerList.AddTower(Instantiate(SniperTower, loc, Quaternion.identity));
                            break;
                    }
                }
            }
        }
    }
}

