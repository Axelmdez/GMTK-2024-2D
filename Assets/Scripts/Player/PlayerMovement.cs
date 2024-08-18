using System.IO.IsolatedStorage;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public LayerMask pickupsLayer;
    public Transform GroundCheckPoint;

    private Rigidbody2D rb;
    private bool isGrounded;

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
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        
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
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            playerAudio.PlayJumpSound();
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
