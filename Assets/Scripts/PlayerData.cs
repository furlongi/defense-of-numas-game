using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // public float[] playerPos;
    public float health;
    public float healthCapacity;
    public int healthTier;

    public int greenGem;
    public int blueGem;
    public int redGem;
    public int purpleGem;

    public int weaponTier;
    

    public PlayerData(Player player, InventoryManager inv, Shooting shooter)
    {
        health = player.health;
        healthCapacity = player.healthCapacity;
        healthTier = player.upgradeTier;

        weaponTier = shooter.upgradeTier;
        
        // playerPos = new float[3];
        // playerPos[0] = player.transform.position.x;
        // playerPos[1] = player.transform.position.y;
        // playerPos[2] = player.transform.position.z;

        greenGem = inv.greenGem;
        blueGem = inv.blueGem;
        redGem = inv.redGem;
        purpleGem = inv.purpleGem;
    }
    
}
