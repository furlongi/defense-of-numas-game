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
        if (other.name == "Player")
        {
            if (currentGem == GemType.GREEN)
            {
                Player.gems += 1 * gemMultiplier;
            }
            else if (currentGem == GemType.BLUE)
            {
                Player.gems += 3 * gemMultiplier;
            }
            else if (currentGem == GemType.PURPLE)
            {
                Player.gems += 5 * gemMultiplier;
            }
            else if (currentGem == GemType.RED)
            {
                Player.gems += 10 * gemMultiplier;
            }
            Debug.Log(Player.gems);
            Destroy(gameObject);
        }
    }
}
