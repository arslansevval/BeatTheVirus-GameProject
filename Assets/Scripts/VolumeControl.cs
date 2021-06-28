using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    
    [SerializeField] string volumeParameter = "MasterVolume";
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;
    [SerializeField] float multiplier = 30f;

    //Detecting Changes in Slider Component
    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
        //toggle.onValueChanged.AddListener(HandleToogleValueChanged);
    }

   
    //Assigning The Value in The Slider to The Mixer
    private void HandleSliderValueChanged(float value)
    {
        mixer.SetFloat(volumeParameter, Mathf.Log10(value) * multiplier);
  
    }

    //Recording the Sound Level
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, slider.value);
    }

    //assigning The Value in The Slider to The Mixer

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(volumeParameter, slider.value); 
    }
}
