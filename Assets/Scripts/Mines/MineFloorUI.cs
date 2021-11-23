using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineFloorUI : MonoBehaviour
{
    public Text floorText;

    private void Start()
    {
        ResetFloor();
    }

    public void UpdateFloor(int floor)
    {
        floorText.text = "Floor " + floor;
    }

    public void ResetFloor()
    {
        floorText.text = "";
    }
}
