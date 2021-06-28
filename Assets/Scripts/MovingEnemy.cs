using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour, IEnemyParent
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;
    bool isColliding = false;
    [SerializeField] int enemyPower = 10;
    [SerializeField] int enemyHealth = 10;
    [SerializeField] AudioClip DieSFX;
    // this will change dynamically
    Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        isColliding = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }

    // this function has only and effect, when the enemy collids with the player 
    // on collision it calls the EnableInvulnerability function to decrease the health of the player 
    // and enable invulnerability, which give the player 3 sec of no collision with enemies 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (isColliding) return;
            isColliding = true;
            FindObjectOfType<PlayerController>().EnableInvulnerability();
        } 
    }

    // deacreases the health of the enemy by the amount "amount"
    // if the health of the enemy is 0 or smaller, the enemy object will be deleted 
    public void DecreaseEnemyHealth(int amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(DieSFX, Camera.main.transform.position);
        }
    }
}
