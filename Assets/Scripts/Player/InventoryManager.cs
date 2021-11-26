using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private const int MAXCount = 999;
    
    public int greenGem;
    public int blueGem;
    public int purpleGem;
    public int redGem;

    // Assign with inspector
    public InventoryTracker tracker;

    
    // Assigns values from all gems and then updates the GUI
    public void LoadGems(int greens, int blues, int purples, int reds)
    {
        greenGem = greens;
        blueGem = blues;
        purpleGem = purples;
        redGem = reds;

        SetGemsToTracker();
    }

    // Gets the necessary info from the save data
    public void LoadFromSave(PlayerData data)
    {
        LoadGems(data.greenGem, data.blueGem, data.redGem, data.purpleGem);
    }

    // Updates the GUI
    public void SetGemsToTracker()
    {
        if (tracker != null)
        {
            tracker.SetGems(greenGem, blueGem, purpleGem, redGem);
        }
    }

    
    public void AddGreen(int count)
    {
        greenGem += count;
        if (greenGem > MAXCount)
        {
            greenGem = MAXCount;
        }
        SetGemsToTracker();
    }
    
    public void AddBlue(int count)
    {
        blueGem += count;
        if (blueGem > MAXCount)
        {
            blueGem = MAXCount;
        }
        SetGemsToTracker();
    }
    
    public void AddRed(int count)
    {
        redGem += count;
        if (redGem > MAXCount)
        {
            redGem = MAXCount;
        }
        SetGemsToTracker();
    }
    
    public void AddPurple(int count)
    {
        purpleGem += count;
        if (purpleGem > MAXCount)
        {
            purpleGem = MAXCount;
        }
        SetGemsToTracker();
    }
    
    /// <summary>
    /// <para>Takes in an array of ints that correspond to specific Gems to increment into:
    /// 
    /// <list type="bullet">
    /// <item>0: Green</item>
    /// <item>1: Blue</item>
    /// <item>2: Red</item>
    /// <item>3: Purple</item>
    /// </list>
    ///
    /// Example:<c>
    /// [20, 4, 0, 1];</c> Adds 20 to Green, 4 to Blue, 0 to Red, 1 to Purple.</para>
    /// 
    /// <para>If size is less than 4, then it will only add to the gem slots up to the slots it gives
    /// values to. If [x, x], then only Green and Blue values get incremented.</para>
    /// </summary>
    /// <param name="gemList">Int array with size up to 4 representing gem slots</param>
    /// <returns></returns>
    public void AddManyGems(int[] gemList)
    {
        int count = 0;
        foreach (int g in gemList)
        {
            if (count > 3)
            {
                break;
            }
            switch (count)
            {
                case 0:
                    AddGreen(g);
                    break;
                case 1:
                    AddBlue(g);
                    break;
                case 2:
                    AddPurple(g);
                    break;
                case 3:
                    AddRed(g);
                    break;
            }
            count++;
        }
    }

    public bool RemoveGreen(int count)
    {
        if (count > greenGem)
        {
            return false;
        }

        greenGem = Math.Max(greenGem-count, 0);
        SetGemsToTracker();
        return true;
    }
    
    public bool RemoveBlue(int count)
    {
        if (count > blueGem)
        {
            return false;
        }

        blueGem = Math.Max(blueGem-count, 0);
        SetGemsToTracker();
        return true;
    }
    
    public bool RemoveRed(int count)
    {
        if (count > redGem)
        {
            return false;
        }

        redGem = Math.Max(redGem-count, 0);
        SetGemsToTracker();
        return true;
    }
    
    public bool RemovePurple(int count)
    {
        if (count > purpleGem)
        {
            return false;
        }

        purpleGem = Math.Max(purpleGem-count, 0);
        SetGemsToTracker();
        return true;
    }

    /// <summary>
    /// <para>Takes in an array of ints that correspond to specific Gems to decrement from:
    /// 
    /// <list type="bullet">
    /// <item>0: Green</item>
    /// <item>1: Blue</item>
    /// <item>2: Red</item>
    /// <item>3: Purple</item>
    /// </list>
    ///
    /// Example:<c>
    /// [20, 4, 0, 1];</c> Removes 20 from Green, 4 from Blue, 0 from Red, 1 from Purple.
    /// It will check if there are enough gems to remove from. If at any point, an element in the
    /// array is greater than the corresponding gem count, then false is returned. Else, it will
    /// decrement the values.
    /// </para>
    /// 
    /// <para>If size is less than 4, then it will only subtract to the gem slots up to the slots it gives
    /// values to. If [x, x], then only Green and Blue values get decremented.</para>
    /// </summary>
    /// <param name="gemList">Int array with size up to 4 representing gem slots</param>
    /// <returns>True if successful transaction of gems. Else, false.</returns>
    public bool GemTransaction(int[] gemList)
    {
        int count = 0;
        foreach (int g in gemList)
        {
            if (count > 3)
            {
                break;
            }
            switch (count)
            {
                case 0:
                    if (g > greenGem)
                    { return false;}
                    break;
                case 1:
                    if (g > blueGem)
                    { return false;}
                    break;
                case 2:
                    if (g > purpleGem)
                    { return false;}
                    break;
                case 3:
                    if (g > redGem)
                    { return false;}
                    break;
            }
            count++;
        }

        count = 0;
        foreach (int g in gemList)
        {
            if (count > 3)
            {
                break;
            }
            switch (count)
            {
                case 0:
                    RemoveGreen(g);
                    break;
                case 1:
                    RemoveBlue(g);
                    break;
                case 2:
                    RemovePurple(g);
                    break;
                case 3:
                    RemoveRed(g);
                    break;
            }
            count++;
        }
        
        return true;
    }

}
