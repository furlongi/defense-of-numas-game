using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class MineManager : MonoBehaviour, IEnemyDeathUpdate
{
    public List<MineFloor> floors;
    public int traveresedFloors;
    public MineFloor currentFloor;
    public float HealPlayerPerFloor = 3f;

    // Assign in inspector
    public Player player;
    public GameObject GreenEnemy;
    public GameObject BlueEnemy;
    public GameObject PurpleEnemy;
    public GameObject RedEnemy;
    public Transform PromptMenuExitMine;
    public Transform ReturnPoint;
    public MineFloorUI floorUI;

    private int _difficulty; // 0: Easy, 1: Medium, 2: Hard
    public int _timer;
    private IEnumerator _countdown;
    

    void Start()
    {
        traveresedFloors = 0;
        currentFloor = null;
        _timer = PlayerPrefs.GetInt("Timer", 600);
        if (_timer < 0) { _timer = 0; }
        floorUI.UpdateTimer(_timer);
    }

    public void ChooseNextFloor()
    {
        if (currentFloor != null)
        {
            currentFloor.ClearFloor();
        }
        int next = Random.Range(0, floors.Count);
        floors[next].Load(this);
        StartTimer();
        currentFloor = floors[next];
        traveresedFloors++;
        floorUI.UpdateFloor(traveresedFloors);
        if (traveresedFloors > 1) {player.Heal(HealPlayerPerFloor);}
    }

    public int Difficulty()
    {
        return _difficulty;
    }

    public void SetDifficulty(int d)
    {
        _difficulty = d;
    }

    public void UpdateDeath(BaseEnemy enemy)
    {
        // Debug.Log(enemy);
        currentFloor.DecrementEnemies();
    }

    public static void DestroyAnEnemy(GameObject obj)
    {
        Destroy(obj);
    }

    public void ShouldExitPrompt()
    {
        PlayerMovement pmove = player.GetComponent<PlayerMovement>();
        pmove.OccupyPlayer();
        PromptMenuExitMine.gameObject.SetActive(true);
    }

    public void ConfirmExit(bool answer)
    {
        PlayerMovement pmove = player.GetComponent<PlayerMovement>();
        PromptMenuExitMine.gameObject.SetActive(false);
        Vector3 vec;
        if (answer)
        {
            StopTimer();
            vec = ReturnPoint.position;
            traveresedFloors = 0;
            currentFloor.ClearFloor();
            currentFloor = null;
            floorUI.ResetFloor();
        }
        else
        {
            vec = currentFloor.playerSpawn.position;
        }
        vec.z = pmove.transform.position.z;
        pmove.transform.position = vec;
        pmove.FreePlayer();
    }

    private void StartTimer()
    {
        if (_countdown == null)
        {
            _countdown = TimerHandler(_timer);
            StartCoroutine(_countdown);
        }
    }

    public void StopTimer()
    {
        if (_countdown != null)
        {
            StopCoroutine(_countdown);
            _countdown = null;
        }
    }

    public void StoreTimer()
    {
        PlayerPrefs.SetInt("Timer", _timer);
        PlayerPrefs.Save();
    }

    IEnumerator TimerHandler(int time)
    {
        yield return new WaitForSeconds(0.3f);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _timer--;
            floorUI.UpdateTimer(_timer);
            if (_timer <= 0)
            {
                floorUI.UpdateTimer(0);
                break;
            }
        }

        _countdown = null;
        OutOfTime();
    }

    private void OutOfTime()
    {
        PlayerMovement pmove = player.GetComponent<PlayerMovement>();
        pmove.OccupyPlayer();
        player.GetComponent<SpriteRenderer>().enabled = false;
        StoreTimer();
        floorUI.ShowOutOfTimeText();
        StartCoroutine(TimeOutTransition());
    }

    IEnumerator TimeOutTransition()
    {
        yield return new WaitForSeconds(4.5f);
        SceneLoader loader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        loader.LoadScene("Hub", "Mines");
    }
}
