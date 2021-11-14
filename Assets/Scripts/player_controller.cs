using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public Animator animator;
    Rigidbody2D rigidbody2d;
    BoxCollider2D playerCollider;
    SpriteRenderer _renderer;
    public float speed = 2.8f;
    public float jumpForce;
    float horizontal;
    float vertical;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    private bool facingRight = true;


    public int maxHealth;
    private int currentHealth;
    //public health_bar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        if (_renderer == null)
        {
            Debug.LogError("Player Sprite is missing a renderer");
        }
        //currentHealth = maxHealth;
        //healthBar.SetMaxhealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if(isGrounded == true) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rigidbody2d.velocity = Vector2.up * jumpForce;
            }
        }
        
        if(isJumping == true && Input.GetKey(KeyCode.Space)) {
            if(jumpTimeCounter > 0) {
                rigidbody2d.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;                
            } else {
                isJumping = false;
            }

        }

        if(Input.GetKeyUp(KeyCode.Space)){
            isJumping = false;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //animator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    void FixedUpdate()
    {
        rigidbody2d.velocity = new Vector2(horizontal * speed, rigidbody2d.velocity.y);
        if (horizontal > 0 && !facingRight)
        {
            // ... flip the player.
            FlipRight();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (horizontal < 0 && facingRight)
        {
            // ... flip the player.
            FlipLeft();
        }
    }


    //public void damage(int damage) {
        //currentHealth -= damage;
        //if(currentHealth <= 0)
        //{
        //    Die();
        //}
        //healthBar.SetHealth(currentHealth);
    //}

    // void Die() {
    //     Destroy(gameObject);
    // }

    void FlipLeft()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;
        _renderer.flipX = true;
    }
    void FlipRight()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;
        _renderer.flipX = false;
    }
}
