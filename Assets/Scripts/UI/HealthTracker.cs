using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{
    // Assign with inspector
    public Text numberText;
    public Image healthBar;
    public Slider slider;
    // -


    public void SetValue(float val)
    {
        slider.value = val;
        numberText.text = val.ToString("0.0");
        SetColor();
    }

    public void SetMaxValue(float val)
    {
        slider.maxValue = val;
    }

    private void SetColor()
    {
        float percent = slider.value / slider.maxValue;
        
        if (percent > 0.55)
        {
            healthBar.color = Color.green;
        }
        else if (percent > 0.3)
        {
            healthBar.color = Color.yellow;
        }
        else if (percent > 0.06)
        {
            healthBar.color = Color.red;
        }
        else
        {
            healthBar.color = Color.red;
        }
    }
}
