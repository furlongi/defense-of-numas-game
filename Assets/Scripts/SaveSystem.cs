using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private const string SavePath = "./player.sav";
    private const string PersistentData = "./session.sav";
    
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        // Debug.Log("SAVING TO " + SavePath);
        FileStream stream = new FileStream(SavePath, FileMode.Create);

        InventoryManager inv = player.GetComponent<InventoryManager>();
        Shooting shoot = player.GetComponentInChildren<Shooting>();
        TowerDataList tList = LoadTowerDataList();
        PlayerData data = new PlayerData(player, inv, shoot, tList);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPLayer()
    {
        // Debug.Log("Loading save file...");
        if (File.Exists(SavePath))
        {
            // Debug.Log("File found!");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(SavePath, FileMode.Open);
            PlayerData player = (PlayerData)formatter.Deserialize(stream);
            PlayerPrefs.SetInt("Wave", player.waveNumber);
            PlayerPrefs.SetInt("Timer", player.timer);
            PlayerPrefs.SetInt("Lives", player.lives);
            PlayerPrefs.Save();
            SavePersistentTower(player.towerList);
            stream.Close();
            return player;
        }
        
        // Debug.Log("No save file found.");
        return null;
    }
    

    public static bool CheckIfSaveExists()
    {
        return File.Exists(SavePath);
    }
    
    
    public static bool CheckIfPersistentExists()
    {
        return File.Exists(PersistentData);
    }

    
    public static void SavePersistentTower(TowerList towerList)
    {
        TowerDataList tlist = new TowerDataList(towerList.towerList.Count);
        
        foreach (var t in towerList.towerList)
        {
            tlist.AddTower(t);
        }
        
        File.WriteAllText(PersistentData, tlist.SaveToString());
    }
    
    public static void SavePersistentTower(TowerData[] towerList)
    {
        if (towerList != null)
        {
            TowerDataList tlist = new TowerDataList(towerList.Length);
        
            foreach (var t in towerList)
            {
                tlist.AddTower(t);
            }
        
            File.WriteAllText(PersistentData, tlist.SaveToString());
        }
    }


    public static TowerDataList LoadTowerDataList()
    {
        if (CheckIfPersistentExists())
        {
            return JsonUtility.FromJson<TowerDataList>(File.ReadAllText(PersistentData));
        }
        
        return null;
    }

    public static void ClearPersistentData()
    {
        if (CheckIfPersistentExists())
        {
            File.Delete(PersistentData);
        }
    }
    
}
