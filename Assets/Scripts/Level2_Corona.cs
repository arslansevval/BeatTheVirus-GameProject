using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_Corona : MonoBehaviour, IEnemyParent
{
    public float speed;
    public float lineOfSite;
    private Transform player;
    bool isColliding = false;
    [SerializeField] int enemyPower = 10;
    [SerializeField] int enemyHealth = 10;
    [SerializeField] AudioClip DieSFX;
    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if ((distanceFromPlayer < lineOfSite))
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position,
                speed * Time.deltaTime);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);

    }

    // this function has only and effect, when the enemy collids with the player 
    // on collision it calls the EnableInvulnerability function to decrease the health of the player 
    // and enable invulnerability, which give the player 3 sec of no collision with enemies 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
/*            if (isColliding) return;
            isColliding = true;*/
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
            myAnimator.SetTrigger("die");    
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(DieSFX, Camera.main.transform.position);
        }
    }
}