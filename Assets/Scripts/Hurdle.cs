using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdle : MonoBehaviour
{
    bool isColliding = false;
    [SerializeField] int hurdlePower = 10;

    void Update()
    {
        isColliding = false;
    }

    // Only the player collide with hurdles 
    // on collistion call EnableInvulnerability to decrease the health of the player 
    // and enable invulnerability, which give the player 3 sec of no collision with enemies 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding) return;
        isColliding = true;
        FindObjectOfType<PlayerController>().EnableInvulnerability();
    }
}
