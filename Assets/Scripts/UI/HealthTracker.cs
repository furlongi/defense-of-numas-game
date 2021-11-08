using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{
    // Assign with inspector
    public Player player;
    public Text numberText;
    public Image healthBar;
    public Slider slider;
    // -

    private float _baseHealth;
    
    private void Start()
    {
        _baseHealth = player.healthCapacity;
        slider.maxValue = _baseHealth;
        slider.value = player.health;
    }
    
    private void Update()
    {
        slider.value = player.health;
    }
}
