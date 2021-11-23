using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
    public int difficultySetting;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerPart"))
        {
            MineManager manager = FindObjectOfType<MineManager>().GetComponent<MineManager>();
            manager.SetDifficulty(difficultySetting);
            manager.ChooseNextFloor();
        }
    }
}
