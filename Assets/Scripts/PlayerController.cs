using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    private float mySpeedX;
    [SerializeField] float speed;
    [SerializeField] float jump_speed;
    private Rigidbody2D myBody;
    private Vector3 default_localscale;
    public bool onGround;
    [SerializeField] bool onAir;
    private bool canDoubleJump;
    private Animator myAnimator;
    [SerializeField] public Fire fire;
    [SerializeField] bool attacked;
    [SerializeField] float current_attack_timer;
    [SerializeField] float default_attackt_timer;
    float counter;
    CapsuleCollider2D playerCollider2D;
    bool isAlive = true;
    [SerializeField] int damage = 10;
    [SerializeField] float invulnerableTime = 3;
    new SpriteRenderer renderer;
    Color playerColor;
    [SerializeField] public Transform fire_point;
    public bool isMoving;
    // Sound Variable
    [SerializeField] AudioClip  FireSFX, HurtSFX, DieSFX, JumpSFX;
    [SerializeField] AudioSource audio;
   

    // ========================================================= Init Player Controller for Testing  

    public GameSession gameSession;
    public IPlayerInputJump PlayerInputJump { get; set; }
    public IPlayerInput PlayerInput { get; set; }
    public IPlayerInputFire PlayerInputFire { get; set; }
    public void InitPlayerInput()
    {
        PlayerInput = new PlayerInput();
    }
    public void InitPlayerInputJump()
    {
        PlayerInputJump = new PlayerInputJump();
    }
    public void InitPlayerInputFire()
    {
        PlayerInputFire = new PlayerInputFire();
    }

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        default_localscale = transform.localScale;
        myAnimator = GetComponent<Animator>();
        attacked = false;
        playerCollider2D = GetComponent<CapsuleCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        playerColor = renderer.material.color;
        counter -= Time.deltaTime;
        gameSession = FindObjectOfType<GameSession>();
        audio = GetComponent<AudioSource>();
       
        
    }

    // Update is called once per frame
    void Update()
    {
        // don't do anything if the player is died! 
        if (!isAlive) 
        { 
            return; 
        }
        move();
        #region Karakterin yüzünün yönünü ayarlar
        if (mySpeedX > 0)
        {
            transform.localScale = new Vector3(default_localscale.x, default_localscale.y, default_localscale.z);
        }
        else if (mySpeedX < 0)
        {
            transform.localScale = new Vector3(-default_localscale.x, default_localscale.y, default_localscale.z);
        }
        #endregion

        /*if(myBody.velocity.x != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (isMoving)
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
            else
            {
                audio.Stop();
            }
            
        
        }*/

        Fire(); 
        if(attacked == true)
        {
            current_attack_timer -= Time.deltaTime;
        }

        else
        {
            current_attack_timer = default_attackt_timer;
        }
        if( current_attack_timer <= 0)
        {
            attacked = false;
        }


        Jump();


       // grounded = Physics2D.OverlapCircle(groundpoint.position, groundradius, whatisground);
        myAnimator.SetBool("grounded", onGround);
        myAnimator.SetFloat("vSpeed", myBody.velocity.y);

      // boþluða düþünce ölme 
        if(this.transform.position.y < -20)
        {
            ProcessHealthDecrease();
        }

    }

    public void move()
    {
        if (PlayerInput == null)
        {
            InitPlayerInput();
        }
        if (onGround == true)
        {
            mySpeedX = PlayerInput.Horizontal; 
            myBody.velocity = new Vector2(mySpeedX * speed, myBody.velocity.y);
            myAnimator.SetFloat("speed", Mathf.Abs(mySpeedX));
        }
        if (onGround == false)
        {
            mySpeedX = PlayerInput.Horizontal;
            myBody.velocity = new Vector2(mySpeedX * (speed - 4f), myBody.velocity.y);
            myAnimator.SetFloat("speed", Mathf.Abs(mySpeedX));
        }
    }

   // sound of player move
    private void footstep_sound()
    {
        audio.Play();
    }
    void Jump()
    {
        if (PlayerInputJump == null)
        {
            InitPlayerInputJump();
        }
        if (PlayerInputJump.Jump)
        {
            if (onGround == true)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jump_speed);
                myAnimator.SetTrigger("jump");
               // AudioSource.PlayClipAtPoint(JumpSFX, Camera.main.transform.position);
                audio.PlayOneShot(JumpSFX);
            }
        }
    }

 

    // Fire code
    void Fire()
    {
        if (PlayerInputFire == null)
        {
            InitPlayerInputFire();
        }
        if (PlayerInputFire.Fire)
        {
            if (attacked == false)
            {
                attacked = true;
                bool isGunEmpty = gameSession.DecreaseAmmo();
                if (!isGunEmpty)
                {
                    //AudioSource.PlayClipAtPoint(FireSFX, Camera.main.transform.position);
                    audio.PlayOneShot(FireSFX);
                    Fire my_fire = Instantiate(fire, fire_point.position, Quaternion.identity);
                    my_fire.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0f);
                    my_fire.transform.parent = GameObject.Find("Fires").transform;
                    if (transform.localScale.x > 0)
                    {
                        my_fire.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0f);
                    } else
                    {
                        Vector3 my_fireScale = my_fire.transform.localScale;
                        my_fire.transform.localScale = new Vector3(-my_fireScale.x, my_fireScale.y, my_fireScale.z);
                        my_fire.GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 0f);
                    }
                }
            }
        }
    }

    // =================================================================== Interaction Code

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = other.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent = null;
    }

    // function to run the coroutine that
    // enable invulnerability, which give the player 3 sec of no collision with enemies 
    public void EnableInvulnerability()
    {
        bool isDied = ProcessHealthDecrease();
        if (!isDied)
        {
            StartCoroutine(GetInvulnerable());
        }
    }

    IEnumerator GetInvulnerable()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Hurtful Things"), LayerMask.NameToLayer("Player"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Hurdles"), LayerMask.NameToLayer("Player"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Player"), true);
        
        playerColor.a = 0.5f; 
        renderer.material.color = playerColor;
        yield return new WaitForSeconds(invulnerableTime);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Hurtful Things"), LayerMask.NameToLayer("Player"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Hurdles"), LayerMask.NameToLayer("Player"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Player"), false);
        
        playerColor.a = 1f;
        renderer.material.color = playerColor; 
    }

    // Decrease the health of the player by calling DecreaseHealth function and checks if the player is dead 
    // if dead, it runs the animation of dying and call ProcessPlayerDeath
    // if not dead, it runs the animation of getting hurt 
    private bool ProcessHealthDecrease()
    {
        bool isDied = gameSession.DecreaseHealth(damage);
        if (isDied)
        {
            //AudioSource.PlayClipAtPoint(DieSFX, Camera.main.transform.position);
            audio.PlayOneShot(DieSFX);
            myAnimator.SetTrigger("die");
            isAlive = false;
            gameSession.ProcessPlayerDeath();
        } else
        {
            //AudioSource.PlayClipAtPoint(HurtSFX, Camera.main.transform.position);
            audio.PlayOneShot(HurtSFX);
            myAnimator.SetTrigger("hurt");
        }
        return isDied; 
    }
}


// ======================================================== player input for moving 


public class PlayerInput: IPlayerInput
{
    public float Horizontal => Input.GetAxis("Horizontal");
}

public interface IPlayerInput
{
    float Horizontal { get;}
}

// ========================================================= player input for jumping 

public class PlayerInputJump : IPlayerInputJump
{
    public bool Jump => Input.GetKeyDown(KeyCode.Space); 
}

public interface IPlayerInputJump
{
    bool Jump { get; }
}

// ========================================================= player input for firing 

public class PlayerInputFire : IPlayerInputFire
{
    public bool Fire => Input.GetKeyDown(KeyCode.DownArrow);
}

public interface IPlayerInputFire
{
    bool Fire { get; }
}