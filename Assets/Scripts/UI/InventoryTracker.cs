using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTracker : MonoBehaviour
{
    // Assign with inspector
    public Text GreenCount;
    public Text BlueCount;
    public Text RedCount;
    public Text PurpleCount;

    public void SetGems(int greens, int blues, int reds, int purples)
    {
        GreenCount.text = greens.ToString();
        BlueCount.text = blues.ToString();
        RedCount.text = reds.ToString();
        PurpleCount.text = purples.ToString();
    }
}
