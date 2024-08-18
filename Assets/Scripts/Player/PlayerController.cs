using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

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


    public Camera cam;
    public LineRenderer lineRenderer;
    public Transform firePoint;
    private bool facingLeft;
    private Vector2 laserTargetDirection;
    public float laserLength = 100;
    public float laserLerpSpeed = 5.0f;
    public float laserMaxAngle = 100.0f;
    public GameObject box;
    private bool isTiming;
    private float timer;
    private Transform hitTransform;
    public float boxTransformTime = 2.0f;
    private bool shrinkMode; 
    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        DisableLaser();
        facingLeft = false;
        isTiming = false;
        shrinkMode = false;

        initialPosition = transform.position;
    }

    void Update()
    {
        Move();
        Jump();
        CheckGround();
        HandleInteraction();
        UpdateGunMode();
        Shoot();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip character sprite based on movement direction
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
                    //heldItem.GetComponent<Rigidbody2D>().isKinematic = true;
                    Rigidbody2D heldRb = heldItem.GetComponent<Rigidbody2D>();
                    heldRb.velocity = Vector2.zero;  
                    heldRb.angularVelocity = 0f;    
                    heldRb.isKinematic = true;
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
    void UpdateGunMode()
    {
        if (Input.GetMouseButtonDown(1))
        {
            shrinkMode = !shrinkMode;
        }
    }
    void Shoot()
    {
        if (heldItem == null) {
            if (Input.GetMouseButtonDown(0))
            {

                // anim.SetTrigger("attackAnimation");
                EnableLaser();
            }

            if (Input.GetMouseButton(0))
            {
                UpdateLaser();

            }

            if (Input.GetMouseButtonUp(0))
            {
                DisableLaser();
            }
        }
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Logic for pushing here
    }

    public void TakeDamage()
    {
        transform.position = initialPosition;
        rb.velocity = Vector2.zero;
        Debug.Log("Player took damage and respawned!");
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

    private void EnableLaser()
    {
        lineRenderer.enabled = true;
    }

    private void UpdateLaser()
    {
        var mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(0,firePoint.position);
        lineRenderer.SetPosition(1, mousePos);
        Vector2 direction = mousePos - (Vector2)firePoint.position;
        direction.Normalize();
        Vector2 characterDirection = facingLeft ? Vector2.left : Vector2.right;
        float angle = Vector2.Angle(characterDirection, direction);
        if (angle > laserMaxAngle)
        {
            float sign = Mathf.Sign(Vector2.SignedAngle(characterDirection, direction));
            Quaternion rotation = Quaternion.AngleAxis(laserMaxAngle * sign, Vector3.forward);
            direction = rotation * characterDirection;
        }
        laserTargetDirection = Vector2.Lerp(laserTargetDirection, direction, laserLerpSpeed * Time.deltaTime);
        
        Vector2 laserEndPoint = (Vector2)firePoint.position + laserTargetDirection * laserLength; 
        lineRenderer.SetPosition(1, laserEndPoint);
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, laserTargetDirection, laserLength, pickupsLayer);
        if (hit) {
            
            lineRenderer.SetPosition(1, hit.point);
            
            if (isTiming && hit.collider.transform == hitTransform)
            {
                timer += Time.deltaTime;

                if (timer >= boxTransformTime)
                {
                    hit.transform.GetComponent<Box>().BoxTransform(hitTransform,shrinkMode);
                    isTiming = false;
                    timer = 0.0f;
                }
            }
            else
            {
                isTiming = true;
                timer = 0.0f;
                hitTransform = hit.collider.transform; 
            }
        }
        else
        {
            isTiming = false;
            timer = 0.0f;
        }

    }

    

    private void DisableLaser()
    {
        lineRenderer.enabled = false;
    }

    private void TurnRight()
    {
        transform.localScale = new Vector3(1, 1, 1);
        facingLeft = false;
    }

    private void TurnLeft()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        facingLeft = true;

    }
}
