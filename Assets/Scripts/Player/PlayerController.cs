using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask pickupsLayer;
    public Transform holdPoint;
    public float holdDistance = 2.0f;
    public float throwForce = 10f;
    public GameObject projectilePrefab;

    private Rigidbody2D rb;
    public bool isGrounded;
    private Box heldItem;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        CheckGround();
        HandleInteraction();
        Shoot();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip character sprite based on movement direction
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            print("is jumping");
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer) || Physics2D.OverlapCircle(groundCheck.position, 0.2f, pickupsLayer);
    }

    void HandleInteraction()
    {
        raycastDirection = transform.right * transform.localScale.x; 

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem == null)
            {
                // Try to pick up a box
                RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, raycastDistance,pickupsLayer);
                if (hit.collider != null && (hit.collider.CompareTag("Throwable") || hit.collider.CompareTag("Liftable")))
                {
                    heldItem = hit.collider.GetComponent<Box>();
                    heldItem.GetComponent<Rigidbody2D>().isKinematic = true;
                    holdPoint.position += Vector3.up * holdDistance;
                    heldItem.transform.position = holdPoint.position;
                    heldItem.transform.parent = transform; 
                }
            }
            else
            {
                // Throw the box
                heldItem.transform.parent = null;
                Rigidbody2D heldRb = heldItem.GetComponent<Rigidbody2D>();
                heldRb.isKinematic = false;
                heldRb.velocity = new Vector2(transform.localScale.x * (((int)heldItem.boxType) < 1 ? throwForce : throwForce/3), 0);
                heldItem = null;
                holdPoint.position -= Vector3.up * holdDistance;
            }
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
           // Logic for shooting here
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Logic for pushing here
    }

    private bool isRaycastVisible = true;
    private Vector2 raycastDirection;
    private float raycastDistance = 1f; 
    void OnDrawGizmos()
    {
        if (isRaycastVisible)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + raycastDirection * raycastDistance);
        }
    }
}
