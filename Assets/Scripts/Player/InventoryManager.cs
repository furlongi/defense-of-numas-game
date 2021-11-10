using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int GreenGem;
    public int BlueGem;
    public int RedGem;
    public int PurpleGem;

    // Assign with inspector
    public InventoryTracker Tracker;

    public void LoadGems(int greens, int blues, int reds, int purples)
    {
        GreenGem = greens;
        BlueGem = blues;
        RedGem = reds;
        PurpleGem = purples;

        SetGemsToTracker();
    }

    public void LoadFromSave(PlayerData data)
    {
        LoadGems(data.greenGem, data.blueGem, data.redGem, data.purpleGem);
    }

    public void SetGemsToTracker()
    {
        if (Tracker != null)
        {
            Tracker.SetGems(GreenGem, BlueGem, RedGem, PurpleGem);
        }
    }

}
