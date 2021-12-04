using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MineFloorUI : MonoBehaviour
{
    public Text floorText;
    public Text timerText;
    public TMP_Text outOfTimeText;
    public TMP_Text outOfTimeSubText;
    public TextPopupFade fader;
    public GameObject teleportAnimation;

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
        floorText.text = "Floor -";
    }

    public void UpdateTimer(int time)
    {
        TimeSpan t = TimeSpan.FromSeconds(time);
        timerText.text = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

    public void ShowOutOfTimeText()
    {
        fader.CreatePopup(outOfTimeText);
        fader.CreatePopup(outOfTimeSubText);
        teleportAnimation.SetActive(true);
    }
}
