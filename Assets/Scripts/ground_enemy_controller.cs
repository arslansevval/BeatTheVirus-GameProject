using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground_enemy_controller : MonoBehaviour, IEnemyParent
{
    public float speed;
    
    
    public bool mustenemy;
    public Transform groundcheck;
    public float distance;

    bool isColliding = false;


    [SerializeField] int enemyPower = 10;
    [SerializeField] int enemyHealth = 10;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector2.right * speed * Time.deltaTime);
       
        RaycastHit2D groundinfo = Physics2D.Raycast(groundcheck.position, Vector2.down, distance);
        /*Debug.Log(groundinfo.collider);*/
        if (groundinfo.collider == false)
        {
          if(mustenemy == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                mustenemy = false;
            } else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                mustenemy = true;
            }
        }
        isColliding = false;
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


    public void DecreaseEnemyHealth(int amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
