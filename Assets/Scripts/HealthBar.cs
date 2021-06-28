using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; 

    public void setSlider(Slider s)
    {
        slider = s;
    }

    // update the healthbar with the value "value"
    public void UpdateHealthBar(int value)
    {
        slider.value = value;
    }
}
