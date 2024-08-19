using System;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform armsHoldingPoint;
    public Transform armsAimPoint;

    public float holdDistance = 2.0f;
    public float throwForce = 10f;
    public LayerMask pickupsLayer;
    public float pickupRange = 3f;
    public SpriteRenderer playerSprite;

    private Box heldItem; 
    private PlayerAudio playerAudio;
    public static event Action tryExit;
    public static event Action boxLifted;

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
            if (heldItem == null)
            {
                TryPickUpBox();

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

    private void TryPickUpBox()
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
                    return;
                }
                else {
                    Debug.Log("Picking it up: Distance" + distanceToBox);
                }
            }  
        }
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

        armsHoldingPoint.gameObject.SetActive(true);
        heldItem.transform.position = armsHoldingPoint.position + Vector3.up * holdDistance;
        heldItem.transform.parent = transform;
        armsAimPoint.gameObject.SetActive(false);
    }

    void ThrowBox()
    { 
        playerAudio.PlayThrowSound();
        heldItem.transform.parent = null;
        Rigidbody2D heldRb = heldItem.GetComponent<Rigidbody2D>();
        heldRb.isKinematic = false;

        var leftOrRight = playerMovement.GetIsLeft() ? -1 : 1;

        heldRb.velocity = (new Vector2(transform.localScale.x * (((int)heldItem.boxType) < 1 ? throwForce * 1.5f : throwForce / 3) * leftOrRight, 1)) ;
        heldItem = null;
        armsHoldingPoint.gameObject.SetActive(false);
        armsAimPoint.gameObject.SetActive(true);
        FlipAllSpriteBack();
        playerAiming.EnableAiming();
    }
}
