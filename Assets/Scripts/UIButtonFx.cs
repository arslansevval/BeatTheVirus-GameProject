using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonFx : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;



    //Button Hover Sound Effect 
    public void HoverSound()
    {
       
        myFx.PlayOneShot(hoverFx);

    }

    //Button Click Sound Effect
    public void ClickSound()
    {
        
        myFx.PlayOneShot(clickFx);

    }
}
