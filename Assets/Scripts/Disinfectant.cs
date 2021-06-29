using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disinfectant : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;

    bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding) return;
        isColliding = true;
        // adding points to the socre
        FindObjectOfType<GameSession>().AddToGun(ammoAmount);

        // gameObject == this
        Destroy(gameObject);
    }

    private void Update()
    {
        isColliding = false;
    }
}
