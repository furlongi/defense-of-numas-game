using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] playerPos;
    public float health;

    public int greenGem;
    public int blueGem;
    public int redGem;
    public int purpleGem;
    

    public PlayerData(Player player, InventoryManager inv)
    {
        health = player.health;
        playerPos = new float[3];
        playerPos[0] = player.transform.position.x;
        playerPos[1] = player.transform.position.y;
        playerPos[2] = player.transform.position.z;

        greenGem = inv.greenGem;
        blueGem = inv.blueGem;
        redGem = inv.redGem;
        purpleGem = inv.purpleGem;
    }
    
}
