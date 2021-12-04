using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool isLoadADeath = false;
    public bool LoadSaveOverride = false; // Prevent a scene from loading a save by default
    public bool ForceSaveLoad = false; //  Force a load save. Overrites above
    
    // Assign with inspector
    public Player player;
    public Shooting shooting;
    public InventoryManager inventory;
    public SceneLoadPosition positionLoad;
    public EventManager towerManager;

    public void Start()
    {
        int loadFrom = PlayerPrefs.GetInt("LoadFromScene", 0);
        if (ForceSaveLoad || loadFrom == 0 && !LoadSaveOverride)
        {
            PlayerPrefs.SetInt("LoadFromScene", 0);
            PlayerPrefs.Save();
            LoadSave();
        }
        else if (isLoadADeath)
        {
            LoadDeath();
            LoadInventory();
            LoadWeapon();
        }
        else if (loadFrom == -1)
        {
            // Do nothing
            PlayerPrefs.SetInt("Wave", 1);
            PlayerPrefs.Save();
        }
        else
        {
            LoadInventory();
            LoadWeapon();
            LoadPlayerHealth();
            if (positionLoad)
            {
                positionLoad.MoveToPosition(PlayerPrefs.GetString("LastScene"));
            }
        }
    }

    public void LoadScene(string scene, string sceneOrigin)
    {
        PlayerPrefs.SetInt("LoadFromScene", 1);
        if (scene.Length != 0)
        {
            PlayerPrefs.SetInt("WeaponTier", shooting.upgradeTier);
            
            StoreInventory();

            PlayerPrefs.SetFloat("Health", player.health);
            PlayerPrefs.SetFloat("HealthCap", player.healthCapacity);
            PlayerPrefs.SetInt("HealthTier", player.upgradeTier);
            PlayerPrefs.SetString("LastScene", sceneOrigin);

            if (towerManager)
            {
                PlayerPrefs.SetInt("Wave", towerManager.waveNumber);
            }
            
            PlayerPrefs.Save();
            
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }

    public void LoadSave()
    {
        PlayerData data = SaveSystem.LoadPLayer();
        if (data != null)
        {
            player.health = data.health;
            player.healthCapacity = data.healthCapacity;
            player.upgradeTier = data.healthTier;
            player.UpdateHealth();
            
            inventory.LoadFromSave(data);
            
            Shooting shooter = player.GetComponentInChildren<Shooting>();
            shooter.upgradeTier = data.weaponTier;
        }
    }

    public void LoadDeath()
    {
        PlayerPrefs.SetFloat("Health", player.healthCapacity);
        StoreBackupInventory();
        SceneManager.LoadScene("Hub", LoadSceneMode.Single);
    }


    public void LoadInventory()
    {
        inventory.greenGem = PlayerPrefs.GetInt("GreenGem", 0);
        inventory.blueGem = PlayerPrefs.GetInt("BlueGem", 0);
        inventory.purpleGem = PlayerPrefs.GetInt("PurpleGem", 0);
        inventory.redGem = PlayerPrefs.GetInt("RedGem", 0);
        inventory.SetGemsToTracker();
    }


    
    public void LoadPlayerHealth()
    {
        player.health = PlayerPrefs.GetFloat("Health", player.health);
        player.healthCapacity = PlayerPrefs.GetFloat("HealthCap", player.healthCapacity);
        player.upgradeTier = PlayerPrefs.GetInt("HealthTier", player.upgradeTier);
        player.UpdateHealth();
    }

    public void LoadWeapon()
    {
        shooting.upgradeTier = PlayerPrefs.GetInt("WeaponTier", shooting.upgradeTier);
    }
    
    public void StoreBackupInventory()
    {
        PlayerPrefs.SetInt("GreenGem", PlayerPrefs.GetInt("GreenGemBak", 0));
        PlayerPrefs.SetInt("BlueGem", PlayerPrefs.GetInt("BlueGemBak", 0));
        PlayerPrefs.SetInt("PurpleGem", PlayerPrefs.GetInt("PurpleGemBak", 0));
        PlayerPrefs.SetInt("RedGem", PlayerPrefs.GetInt("RedGemBak", 0));
        PlayerPrefs.Save();
        LoadInventory();
    }

    public void StoreInventory()
    {
        PlayerPrefs.SetInt("GreenGemBak", player.inventory.greenGem);
        PlayerPrefs.SetInt("BlueGemBak", player.inventory.blueGem);
        PlayerPrefs.SetInt("PurpleGemBak", player.inventory.purpleGem);
        PlayerPrefs.SetInt("RedGemBak", player.inventory.redGem);
        
        PlayerPrefs.SetInt("GreenGem", player.inventory.greenGem);
        PlayerPrefs.SetInt("BlueGem", player.inventory.blueGem);
        PlayerPrefs.SetInt("PurpleGem", player.inventory.purpleGem);
        PlayerPrefs.SetInt("RedGem", player.inventory.redGem);
    }
    
}
