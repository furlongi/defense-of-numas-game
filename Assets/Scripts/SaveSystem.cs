using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private const string SavePath = "./player.sav";
    
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        Debug.Log("SAVING TO " + SavePath);
        FileStream stream = new FileStream(SavePath, FileMode.Create);

        PlayerData data = new PlayerData(player);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPLayer()
    {
        Debug.Log("Loading save file...");
        if (File.Exists(SavePath))
        {
            Debug.Log("File found!");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(SavePath, FileMode.Open);
            PlayerData player = (PlayerData)formatter.Deserialize(stream);
            stream.Close();
            return player;
        }
        
        Debug.Log("No save file found.");
        return null;
    }



}
