using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 20;
    public float healthCapacity = 20;
    public int upgradeTier = 0;
    
    // Assign with inspector
    public HealthTracker healthBar;
    public InventoryManager inventory;

    void Start()
    {
        if (healthBar != null)
        {
            healthBar.SetMaxValue(healthCapacity);
            healthBar.SetValue(health);
        }

        if (inventory != null)
        {
            inventory.SetGemsToTracker();
        }
    }

    public void Damage(float damage)
    {
        float newHealth = health - damage;
        SetNewHealth( newHealth >= 0? newHealth : 0 );
        if (health <= 0)
        {
            Debug.Log("Game over! (Death not implemented yet)");
        }
    }

    public void Heal(float heal)
    {
        float newHealth = health + heal;
        SetNewHealth( newHealth > healthCapacity? healthCapacity : newHealth);
    }

    public void HealMax()
    {
        SetNewHealth(healthCapacity);
    }

    private void SetNewHealth(float h)
    {
        health = h;
        if (healthBar != null)
        {
            healthBar.SetValue(health);
        }
    }

    public void UpgradeHealth(float h)
    {
        healthCapacity += h;
        health += h;
        healthBar.SetMaxValue(healthCapacity);
        healthBar.SetValue(health);
    }

    public void UpdateHealth()
    {
        healthBar.SetMaxValue(healthCapacity);
        healthBar.SetValue(health);
    }
}
