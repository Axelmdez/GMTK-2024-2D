using System;
using Unity.Burst.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteraction : MonoBehaviour
{
    public Transform armsHoldingPoint;
    public Transform armsAimPoint;
    [SerializeField] BoxCollider2D smallCollider;
    [SerializeField] BoxCollider2D mediumCollider;

    public float holdDistance = 2.0f;
    public float throwForce = 10f;
    public LayerMask pickupsLayer;
    public float pickupRange = 3f;
    public SpriteRenderer playerSprite;

    private Box heldItem; 
    private PlayerAudio playerAudio;
    public static event Action tryExit;
    public static event Action boxLifted;
    public float playerMassSBox = 3f;
    public float playerMassMBox = 5f;

    private PlayerAiming playerAiming;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAiming = GetComponent<PlayerAiming>();
        playerAudio = GetComponent<PlayerAudio>();
    }
    void Update()
    {
        HandleInteraction();
        if (heldItem != null) {
            FlipAllSprite();
        }
    }

    private void HandleInteraction()
    { 
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool didYouEvenLift;

            if (heldItem == null)
            {
                didYouEvenLift = TryPickUpBox();
            }
            else
            {
                ThrowBox(); 
            }
            tryExit?.Invoke();  
            boxLifted?.Invoke();
        }
    }

    private void FlipAllSpriteBack()
    {
        bool needFlip = false;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        needFlip = mousePosition.x < transform.position.x;

        if (needFlip)
        {
            playerSprite.flipX = true;
        }
        else {
            playerSprite.flipX = false;
        }

    }

    private void FlipAllSprite()
    {   
        float direction = Input.GetAxis("Horizontal");
        bool needFlip = false;
        if (direction < 0)
        {
            needFlip = true;
        }
        else if (direction == 0) {
            return;
        }
        if (playerSprite != null) 
        {
            playerSprite.flipX = needFlip;
        }
    }

    public bool HoldingItem()
    {
        return heldItem != null;
    } 

    private bool TryPickUpBox()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(mousePosition, 0.1f, pickupsLayer);

        foreach (Collider2D collider in hitColliders)
        {
            Box box = collider.GetComponent<Box>();
            if (box != null)
            {
                float distanceToBox = Vector2.Distance(transform.position, box.transform.position);
                if (distanceToBox <= pickupRange && (collider.CompareTag("Throwable") || collider.CompareTag("Liftable")))
                {
                    PickUpBox(box);
                    return true;
                }
                
            }  
        }
        return false;
    }

    private void PickUpBox(Box box)
    {
        playerAudio.PlayPickupSound();
        heldItem = box;
        Rigidbody2D heldRb = heldItem.GetComponent<Rigidbody2D>(); 
        heldRb.velocity = Vector2.zero;
        heldRb.angularVelocity = 0f;
        heldRb.isKinematic = true;

        playerAiming.DisableAiming();
        
        holdDistance = heldItem.boxType == BoxType.small ? 0f : .5f;

        Rigidbody2D player = transform.GetComponent<Rigidbody2D>();

        if (heldItem.boxType != BoxType.large)
        {
            player.mass += playerMassSBox;
        } 
         
        armsHoldingPoint.gameObject.SetActive(true);
        
        if(box.boxType == BoxType.small) smallCollider.gameObject.SetActive(true); 
        else mediumCollider.gameObject.SetActive(true);
        
        heldItem.transform.position = armsHoldingPoint.position + Vector3.up * holdDistance;
        heldItem.transform.rotation = Quaternion.identity;
        heldItem.transform.parent = transform;
        armsAimPoint.gameObject.SetActive(false);

    }

    void ThrowBox()
    {
        Rigidbody2D player = GetComponent<Rigidbody2D>();
         
        if (heldItem.boxType != BoxType.large)
        {
            player.mass -= playerMassSBox;
        }

        if (heldItem.boxType == BoxType.small) smallCollider.gameObject.SetActive(false);
        else mediumCollider.gameObject.SetActive(false);
         
        playerAudio.PlayThrowSound();
         
        heldItem.transform.parent = null;
        Rigidbody2D heldRb = heldItem.GetComponent<Rigidbody2D>();
        heldRb.isKinematic = false;
         
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = transform.position;
        Vector2 throwDirection = (cursorPosition - playerPosition).normalized;
         
        float distanceMultiplier = Vector2.Distance(playerPosition, cursorPosition);

         
        float minMultiplier = 0.5f;  
        float maxMultiplier = 2.0f;  
        distanceMultiplier = Mathf.Clamp(distanceMultiplier, minMultiplier, maxMultiplier);

     
        float adjustedThrowForce = throwForce * distanceMultiplier;
        heldRb.velocity = throwDirection * adjustedThrowForce;
 
        heldItem = null;
        armsHoldingPoint.gameObject.SetActive(false);
        armsAimPoint.gameObject.SetActive(true);
        FlipAllSpriteBack();
        playerAiming.EnableAiming();
        

    }
     
}
