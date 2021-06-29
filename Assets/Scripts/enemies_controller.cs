using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemies_controller : MonoBehaviour, IEnemyParent
{
    public Transform cpos1, cpos2;
    public float speed;
    public Transform startPos;
    Vector3 nextPos;
    bool isColliding = false;
    [SerializeField] int enemyPower = 10;
    [SerializeField] int enemyHealth = 10;
    [SerializeField] AudioClip DieSFX;

    // Start is called before the first frame update
    void Start()
    {
        cpos1.position = new Vector3(cpos1.position.x, cpos1.position.y);
        cpos2.position = new Vector3(cpos2.position.x, cpos2.position.y);
        nextPos = startPos.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == cpos1.position)
        {
            nextPos = cpos2.position;
        }
        if (transform.position == cpos2.position)
        {
            nextPos = cpos1.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, 
            nextPos, speed * Time.deltaTime);
        isColliding = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(cpos1.position, cpos2.position);
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
            AudioSource.PlayClipAtPoint(DieSFX, Camera.main.transform.position);
        }
    }

}
