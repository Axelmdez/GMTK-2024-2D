using System.IO.IsolatedStorage;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float moveSpeed = 9f;
    public float jumpForce = 10f;
    public float hangTime = .3f;
    public float jumpBufferLength = .5f;
    public float lowJumpGravity = 2f;
    public float defaultGravity = 3f;
    public LayerMask groundLayer;
    public LayerMask pickupsLayer;
    public Transform GroundCheckPoint;
    
    public float acceleration = 500f;    
    public float deceleration = 500f;     

    private Rigidbody2D rb;
    private bool isGrounded;
    private float hangTimer;
    private float jumpBufferTimer;
    private float currentSpeed;

    private PlayerAudio playerAudio;

    void Start()
    {
        playerAudio = GetComponent<PlayerAudio>();
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        Move();
        Jump();
        CheckGround();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput != 0)
        {
            // Apply acceleration when there's input
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            // Apply deceleration when there's no input
            currentSpeed -= deceleration * Time.deltaTime;
        }

        // Clamp the speed to ensure it doesn't exceed maxSpeed or drop below 0
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, moveSpeed);

        // Apply the calculated speed to the rigidbody velocity
        rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);

        // If there's no input and the speed is very small, set the speed to 0
        if (moveInput == 0 && currentSpeed < 0.1f)
        {
            currentSpeed = 0f;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        
        if (rb.velocity.magnitude > 0 && isGrounded) playerAudio.PlayWalkSound(); 

        if (moveInput > 0)
        {
            TurnRight();
        }
        else if (moveInput < 0)
        {
            TurnLeft();
        }
    }

    void Jump()
    {
        // Gravity Scale
        if (rb.velocity.y > 0)
        {
            rb.gravityScale = lowJumpGravity;
        }
        else {
            rb.gravityScale = defaultGravity;
        }
        // Hang Time
        if (isGrounded)
        {
            hangTimer = hangTime;
        }
        else { 
            hangTimer -= Time.deltaTime;
        }

        // Jump Buffer
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimer = jumpBufferLength;
        }
        else { 
            jumpBufferTimer -= Time.deltaTime;
        }

        if (jumpBufferTimer >= 0f && hangTimer >= 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferTimer = 0;
            Debug.Log("is jumping");
            playerAudio.PlayJumpSound();
        }
        // Small Jump
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
            Debug.Log("is jumping");
            
        }
    }
     
    void CheckGround()
    {
        var wasInAir = !isGrounded == true; 

        isGrounded = Physics2D.OverlapCircle(GroundCheckPoint.position, 0.2f, groundLayer) || Physics2D.OverlapCircle(GroundCheckPoint.position, 0.2f, pickupsLayer);

        if ((wasInAir && isGrounded))
        {
            playerAudio.PlayLandingSound();
        }

    }

    private void TurnRight()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void TurnLeft()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }
}
