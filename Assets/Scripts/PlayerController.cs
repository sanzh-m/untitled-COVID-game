using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private enum State {idle, running, jumping, falling, hurt, climb};
    private State state = State.idle;
    private Collider2D coll;

    //Ladder variables
    [HideInInspector] public bool canClimb = false;
    [HideInInspector] public bool bottomLadder = false;
    [HideInInspector] public bool topLadder = false;
    public Ladder ladder;
    private float naturalGravity;

    [SerializeField] private float climbSpeed = 3f;


    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float hurtForce = 10f;
    
    [SerializeField] private AudioSource collectibleSound;
    [SerializeField] private AudioSource footstep;

    [SerializeField] private int health;
    [SerializeField] private TextMeshProUGUI healthAmount;

    protected void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        healthAmount.text = health.ToString();
        naturalGravity = rb.gravityScale;
    }


    private void Update() {

        if (state == State.climb)
        {
            Climb();
        }

        else if (state != State.hurt) {
            Movement();
        }

        AnimationState();
        anim.SetInteger("state", (int) state);
    }

    // Playing sound on collectible scripts size will result in issues
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectible")
        {
            collectibleSound.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.JumpedOn();
                Jump();

            } 
            
            else 
            {

                state = State.hurt;
                HandleDamage(other);
            }
        } 
        
        else if (other.gameObject.tag == "IndestructibleEnemy")

        {
            state = State.hurt;
            HandleDamage(other);
        }   

        else if (other.gameObject.tag == "Virus")

        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }

    private void HandleDamage(Collision2D other)
    {
        HandleHealth();
        if (other.gameObject.transform.position.x > transform.position.x)
        {
            //Enemy is to my right -> damaged and shift left
            rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
        }
        else
        {
            //Enemy is to my left -> damaged and shift right
            rb.velocity = new Vector2(hurtForce, rb.velocity.y);
        }
    }

    private void HandleHealth()
    {
        health -= 1;
        healthAmount.text = health.ToString();
        if (health <= 0) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Movement() 
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (canClimb && Mathf.Abs(Input.GetAxis("Vertical")) > .1f)
        {
            state = State.climb;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            transform.position = new Vector3(ladder.transform.position.x, rb.position.y);
            rb.gravityScale = 0f;
        }

        if (hDirection < 0) 
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        } 

        else if (hDirection > 0) 
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        // Prevent sliding effect once user releases the key
        if (Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground)) 
        {
      
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }

    private void Climb()
    {

        if (Input.GetButtonDown("Jump")) 
        {
            rb.constraints =  RigidbodyConstraints2D.FreezeRotation;
            canClimb = false;
            rb.gravityScale = naturalGravity;
            anim.speed = 1f;
            Jump();
            return;
        }
    
        float vDriection = Input.GetAxis("Vertical");

        if (vDriection > .1f && !topLadder)
        {
            // climbing up
            rb.velocity = new Vector2(0f, vDriection * climbSpeed);
            anim.speed = 1f;
        }

        else if (vDriection < -.1f && !bottomLadder)
        {
            // climbing down
            rb.velocity = new Vector2(0f, vDriection * climbSpeed);
            anim.speed = 1f;
        }
        else
        {
            //still
            anim.speed = 0f;
            rb.velocity = Vector2.zero;
        }
    }

    private void AnimationState()
    {

        if (state == State.climb)
        {
            
        }
        else if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            } 
        }

        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }

        else if (state ==  State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }

        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Moving
            state = State.running;

        }

        else 
        {
            state = State.idle;
        }
        
    }

    private void Footstep()
    {
        footstep.Play();
    }
}
