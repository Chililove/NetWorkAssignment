using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(float healthbar)
    {
        slider.maxValue = healthbar;
        slider.value = healthbar;

       fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float healthBar)
    {
        slider.value = healthBar;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
