using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hygiene_p : MonoBehaviour
{
    // effectRetio is how much will the hygiene prodcut will effect power of the 
    [SerializeField] float effectRetio = 0.5f;
    // how long of the hygine product will last 
    [SerializeField] int activationTime = 10; 
    bool isColliding = false;

    // only player collide with Hygiene_p
    // on collision call EnableHygieneP to enable the shield with will decrease the 
    // the power of the enemies by retio of "effectRetio" for "activationTime"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding) return;
        isColliding = true;
        
        FindObjectOfType<GameSession>().EnableHygieneP(effectRetio, activationTime);

        // gameObject == this
        Destroy(gameObject);
    }

    private void Update()
    {
        isColliding = false;
    }
}
