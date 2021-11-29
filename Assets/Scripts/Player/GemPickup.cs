using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickup : MonoBehaviour
{
    public enum GemType{GREEN, BLUE, RED, PURPLE};
    public GemType currentGem; 
    public int gemMultiplier;
    
    private bool _beenPicked = false;

    private void Update()
    {
        if (_beenPicked)
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if (player)
            {
                switch (currentGem)
                {
                    case GemType.GREEN:
                        player.inventory.AddGreen(gemMultiplier);
                        break;
                    case GemType.BLUE:
                        player.inventory.AddBlue(gemMultiplier);
                        break;
                    case GemType.PURPLE:
                        player.inventory.AddPurple(gemMultiplier);
                        break;
                    case GemType.RED:
                        player.inventory.AddRed(gemMultiplier);
                        break;
                }
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!_beenPicked && other.gameObject.CompareTag("PlayerPart"))
        {
            _beenPicked = true;
        }
    }
}
