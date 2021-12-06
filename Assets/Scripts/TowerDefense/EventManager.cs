using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;


public class EventManager : MonoBehaviour
{
    
    public int waveNumber;
    public int totalPopulation;
    public int currentPopulation;
    private BaseTower _currentlySelectedTower;
    
    [NonSerialized] public List<Renderer> TowerRadiuses = new List<Renderer>();
    [NonSerialized] public bool CurrentPopulationModified = false;
    [NonSerialized] public Text RoundCounter;
    [NonSerialized] public Text LivesCounter;
    [NonSerialized] public GameObject TowerDefenseUI;
    [NonSerialized] public GameObject TowerShop;
    [NonSerialized] public UpgradeTower TowerUpgradeShop;
    [NonSerialized] public Camera Camera;
    [NonSerialized] public GameObject sceneExiter;
    [NonSerialized] public bool IsOver;
    [NonSerialized] public TMP_Text GameWonText;
    
    public GameObject greenTowerEnemy;
    public GameObject blueTowerEnemy;
    public GameObject purpleTowerEnemy;
    public GameObject redTowerEnemy;
    public GameObject enemySpawner;
    public GameOverHandler gameOverHandler;

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
        TowerUpgradeShop = TowerDefenseUI.transform.Find("Tower Upgrade Shop").GetComponent<UpgradeTower>();
        GameWonText = TowerDefenseUI.transform.Find("GameWonUI").transform.GetChild(0).GetComponent<TMP_Text>();
        sceneExiter = GameObject.Find("Scene Exiter");
        IsOver = false;
        _wave = gameObject.AddComponent<Wave>();
        waveNumber = PlayerPrefs.GetInt("Wave", 1);

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
            if (currentPopulation <= 0 && !IsOver)
            {
                IsOver = true;
                RoundCounter.text = "Game Over...";
                EndWave();
            }
        }

        if (!IsOver && sceneExiter.activeSelf)
        {
            sceneExiter.SetActive(false);
        }
        else if (IsOver && !sceneExiter.activeSelf)
        {
            if (waveNumber == 4)
            {
                RoundCounter.text = "";
                LivesCounter.text = "";
                TextPopupFade winText = gameObject.AddComponent<TextPopupFade>();
                CancelPopups();
                winText.timeToFade = 10f;
                winText.CreatePopup(GameWonText);
            }
            else
            {
                RoundCounter.text = "Wave Complete!";
            }
            sceneExiter.SetActive(true);
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

        gameOverHandler.HandleGameOver();
    }
    

    public void HandleUserClick()
    {
        
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Vector2.zero, 100);
        bool shouldCancelPopups = true;
        
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.CompareTag("TowerUpgradeShop"))
            {
                CancelPopups();
                TowerUpgradeShop.gameObject.SetActive(true);
                shouldCancelPopups = false;
            }
            if (hits[i].transform.CompareTag("TowerShop"))
            {
                CancelPopups();
                TowerShop.SetActive(true);
                shouldCancelPopups = false;
            }
            else if (hits[i].transform.CompareTag("TowerHitbox"))
            {
                Transform parentTransform = hits[i].transform.parent;
                CancelPopups();
                parentTransform.Find("Radius").GetComponent<Renderer>().enabled = true;
                TowerUpgradeShop.SetTower(parentTransform.gameObject.GetComponent<BaseTower>());
                TowerUpgradeShop.gameObject.SetActive(true);
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
        TowerUpgradeShop.gameObject.SetActive(false);
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
                    BaseTower t;
                    switch (towerData.type)
                    {
                        case (int)TowerType.Heavy:
                            t = Instantiate(HeavyTower, loc, Quaternion.identity);
                            t.SetTier(towerData.tier);
                            towerList.AddTower(t);
                            break;
                        case (int)TowerType.Normal:
                            t = Instantiate(NormalTower, loc, Quaternion.identity);
                            t.SetTier(towerData.tier);
                            towerList.AddTower(t);
                            break;
                        case (int)TowerType.Sniper:
                            t = Instantiate(SniperTower, loc, Quaternion.identity);
                            t.SetTier(towerData.tier);
                            towerList.AddTower(t);                            
                            break;
                    }
                }
            }
        }
    }
}

