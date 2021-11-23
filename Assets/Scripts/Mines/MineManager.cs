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
    

    void Start()
    {
        traveresedFloors = 0;
        currentFloor = null;
    }

    public void ChooseNextFloor()
    {
        if (currentFloor != null)
        {
            currentFloor.ClearFloor();
        }
        int next = Random.Range(0, floors.Count);
        floors[next].Load(this);
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
        Debug.Log(enemy);
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
            vec = ReturnPoint.position;
            traveresedFloors = 0;
            currentFloor.ClearFloor();
            currentFloor = null;
            floorUI.ResetFloor();
        }
        else
        {
            vec = currentFloor.transform.position;
        }
        vec.z = pmove.transform.position.z;
        pmove.transform.position = vec;
        pmove.FreePlayer();
    }
}
