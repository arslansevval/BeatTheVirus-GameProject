using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mix_ground_enemy : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidbody2D;
    BoxCollider2D boxCollider2D;
    bool isFacingRight;
  
 

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsFacingRight())
        {
            rigidbody2D.velocity = new Vector2(speed, 0f);
        } else
        {
            rigidbody2D.velocity = new Vector2(-speed, 0f);
        }
       
       
    }
    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2D.velocity.x)), transform.localScale.y);
    }
}
