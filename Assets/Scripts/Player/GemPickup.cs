using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickup : MonoBehaviour
{
    public enum GemType{GREEN, BLUE, RED, PURPLE};
    public GemType currentGem; 
    public int gemMultiplier;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.GetComponentInParent<Player>();
            if (currentGem == GemType.GREEN)
            {
                player.inventory.AddGreen(gemMultiplier);
            }
            else if (currentGem == GemType.BLUE)
            {
                player.inventory.AddBlue(gemMultiplier);
            }
            else if (currentGem == GemType.PURPLE)
            {
                player.inventory.AddPurple(gemMultiplier);
            }
            else if (currentGem == GemType.RED)
            {
                player.inventory.AddRed(gemMultiplier);
            }
            
            Destroy(gameObject);
        }
    }
}
