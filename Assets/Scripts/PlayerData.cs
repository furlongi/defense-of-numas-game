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
    public int waveNumber;
    public int lives;

    public int timer;

    public TowerData[] towerList;
    

    public PlayerData(Player player, InventoryManager inv, Shooting shooter, TowerDataList tList)
    {
        health = player.health;
        healthCapacity = player.healthCapacity;
        healthTier = player.upgradeTier;

        weaponTier = shooter.upgradeTier;

        greenGem = inv.greenGem;
        blueGem = inv.blueGem;
        redGem = inv.redGem;
        purpleGem = inv.purpleGem;

        waveNumber = PlayerPrefs.GetInt("Wave", 1);
        timer = PlayerPrefs.GetInt("Timer", 600);
        lives = PlayerPrefs.GetInt("Lives", 50);

        if (tList != null)
        {
            towerList = tList.towerList;
        }
    } 
    
}
