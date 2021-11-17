using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MineManager : MonoBehaviour, IEnemyDeathUpdate
{
    public MineFloor[] floors;
    public int traveresedFloors;
    public MineFloor currentFloor;

    // Assign in inspector
    public Player player;
    public GameObject GreenEnemy;
    public GameObject BlueEnemy;
    public GameObject PurpleEnemy;
    public GameObject RedEnemy;
    
    private int _difficulty;
    
    // Start is called before the first frame update
    void Start()
    {
        traveresedFloors = 0;
        currentFloor = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseNextFloor()
    {
        if (currentFloor != null)
        {
            currentFloor.ClearFloor();
        }
        int next = Random.Range(0, floors.Length);
        floors[next].Load(this);
        currentFloor = floors[next];
        traveresedFloors++;
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

    public GameObject SpawnEnemy(SpawnRates.EnemyType enemyType, Transform trform)
    {
        GameObject go;
        switch (enemyType)
        {
            case SpawnRates.EnemyType.Green:
                go = Instantiate(GreenEnemy, transform.position, trform.rotation);
                break;
            case SpawnRates.EnemyType.Blue:
                go = Instantiate(BlueEnemy, trform.position, trform.rotation);
                break;
            case SpawnRates.EnemyType.Purple:
                go = Instantiate(PurpleEnemy, trform.position, trform.rotation);
                break;
            case SpawnRates.EnemyType.Red:
                go = Instantiate(RedEnemy, trform.position, trform.rotation);
                break;
            default:
                go = Instantiate(GreenEnemy, trform.position, trform.rotation);
                break;
        }

        return go;
    }

    public static void DestroyAnEnemy(GameObject obj)
    {
        Destroy(obj);
    }
}
