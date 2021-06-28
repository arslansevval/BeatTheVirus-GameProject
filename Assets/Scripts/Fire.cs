using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Fire : MonoBehaviour
{
    [SerializeField] int firePower = 10; 
    bool isColliding = false;
    [SerializeField] GameObject effect;

    // fire only has an effect if it collide with an enemy 
    // on collision the fire object is deleted (followed by a destruction effect) and health of the
    // enemy object is decreased by calling the DecreaseEnemyHealth by the amount of firePower. 
    // on no collision the fire object is deleted (followed by a destruction effect)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding) return;
        isColliding = true;
        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Enemies")))
        { 
            collision.gameObject.GetComponent<IEnemyParent>().DecreaseEnemyHealth(firePower);
            Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        isColliding = false;
    }
}