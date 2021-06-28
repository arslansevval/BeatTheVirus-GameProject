using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX; 
    [SerializeField] int pointsForCoinPickup = 1;
    [SerializeField] float volume = 0.1f;
    [SerializeField] AudioSource myFX ;
    
    bool isColliding = false;

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding) return;
        isColliding = true; 
        // adding points to the socre
        FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);

        // playing the sound effect 
        // AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position, volume); 
        myFX.PlayOneShot(coinPickUpSFX);
            
        // Delete the coin
        Destroy(gameObject); 
    }

    private void Update()
    {
        isColliding = false;
    }
}
