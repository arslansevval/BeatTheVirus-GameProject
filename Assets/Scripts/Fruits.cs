using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    [SerializeField] int healthAmount = 5;
    [SerializeField] GameObject effect;
    bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding) return;
        isColliding = true;
        // adding points to the socre
        FindObjectOfType<GameSession>().AddToHealth(healthAmount);

        // Deleteing the booster 
        Destroy(gameObject);
    }

    private void Update()
    {
        isColliding = false;
    }
}
