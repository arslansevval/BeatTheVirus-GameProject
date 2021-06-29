using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_enemy : MonoBehaviour, IEnemyParent
{
    public float speed;
    public float lineOfSite;
    public float shootinRange;
    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;
    public float fireRate = 5f;
    private float nextFireTime;
    [SerializeField] AudioClip FireSFX, DieSFX;
    bool isColliding = false;
    [SerializeField] int enemyPower = 10;
    [SerializeField] int enemyHealth = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if ((distanceFromPlayer < lineOfSite) && (distanceFromPlayer > shootinRange))
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position,
                speed * Time.deltaTime);
           

        } 
        else if (distanceFromPlayer <= shootinRange && nextFireTime < Time.time){
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
            AudioSource.PlayClipAtPoint(FireSFX, Camera.main.transform.position);
        }
        isColliding = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootinRange);

    }
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


