using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{

    // Assign using the inspector
    public Player player;
    public InventoryManager inventory;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l")) // L
        {
            LoadData();
        }
    }


    public void LoadData()
    {
        PlayerData data = SaveSystem.LoadPLayer();
        if (data != null)
        {
            player.health = data.health;
            player.healthCapacity = data.healthCapacity;
            player.upgradeTier = data.healthTier;
            
            player.transform.position = new Vector3(
                data.playerPos[0],
                data.playerPos[1],
                data.playerPos[2]);
            inventory.LoadFromSave(data);
            
            Shooting shooter = player.GetComponentInChildren<Shooting>();
            shooter.upgradeTier = data.weaponTier;
        }
    }
}
