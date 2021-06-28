using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class GunBar : MonoBehaviour
{
    public Slider slider;
    public void setSlider(Slider s) {
        slider = s; 
    }
    float defaultMaxValue; 

    private void Start()
    {
        slider = GetComponent<Slider>();
        defaultMaxValue = slider.maxValue; 
    }

    // updates the gunbar to be the equal to "value"
    // in order for the gunbar to work dynamiclly, the max value of the bar is updated when the new 
    // gun value is bigger than the older one 
    // if the gun value is 0 the max value of the gunbar is resetted to a default value. 
    public void UbdateGunBar(int value)
    {
        if(value > slider.maxValue)
        {
            slider.maxValue = value;
        }else if(value == 0)
        {
            slider.maxValue = defaultMaxValue; 
        }
        slider.value = value; 
    }
}
