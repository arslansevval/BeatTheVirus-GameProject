using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour

{
    public Slider slider;

    public void setSlider(Slider s)
    {
        slider = s; 
    }

    private void Start()
    {
        slider = GetComponent<Slider>(); 
    }

    // update the shieldbar with the value "value" 
    public void UpdateShieldBar(int value)
    {
        slider.value = value;
    }

    // this function should be called to hide the shieldbar if the time of shield has been extended 
    public void DeactivateShieldBar()
    {
        gameObject.SetActive(false); 
    }

    // this function should be called to show the shieldbar if the player has picked up a hygiene product 
    public void ActivateShieldBar()
    {
        gameObject.SetActive(true);
    }

}
