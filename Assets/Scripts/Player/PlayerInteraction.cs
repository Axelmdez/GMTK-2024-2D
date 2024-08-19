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


    private Box heldItem;
    private Rigidbody2D rb;
    private Vector2 raycastDirection;
    private float raycastDistance = 1f;
    private PlayerAudio playerAudio;

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
        }
    }

    void OldHandleInteraction()
    {
        raycastDirection = transform.right * transform.localScale.x;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem == null)
            {
                // Try to pick up a box
                RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, raycastDistance, pickupsLayer);
                if (hit.collider != null && (hit.collider.CompareTag("Throwable") || hit.collider.CompareTag("Liftable")))
                {
                    playerAudio.PlayPickupSound();
                    heldItem = hit.collider.GetComponent<Box>();
                    Rigidbody2D heldRb = heldItem.GetComponent<Rigidbody2D>();
                    heldRb.velocity = Vector2.zero;
                    heldRb.angularVelocity = 0f;
                    heldRb.isKinematic = true;

                    playerAiming.DisableAiming();

                    holdDistance = heldItem.boxType == BoxType.small ? 1.5f : 2f;
                     
                    armsHoldingPoint.gameObject.SetActive(true);
                    heldItem.transform.position = armsHoldingPoint.position + Vector3.up * holdDistance;
                    heldItem.transform.parent = transform;
                    armsAimPoint.gameObject.SetActive(false);
                }
            }
            else
            {
                // Throw the box
                playerAudio.PlayThrowSound();
                heldItem.transform.parent = null;
                Rigidbody2D heldRb = heldItem.GetComponent<Rigidbody2D>();
                heldRb.isKinematic = false;
                heldRb.velocity = new Vector2(transform.localScale.x * (((int)heldItem.boxType) < 1 ? throwForce : throwForce / 3), 0);
                heldItem = null; 
                armsHoldingPoint.gameObject.SetActive(false); 
                armsAimPoint.gameObject.SetActive(true);
                playerAiming.EnableAiming();
            }
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
                if (distanceToBox <= pickupRange)
                {
                    PickUpBox(box);
                    return;
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

        holdDistance = heldItem.boxType == BoxType.small ? 1.5f : 2f;

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

        heldRb.velocity = (new Vector2(transform.localScale.x * (((int)heldItem.boxType) < 1 ? throwForce : throwForce / 3) * leftOrRight, 0)) ;
        heldItem = null;
        armsHoldingPoint.gameObject.SetActive(false);
        armsAimPoint.gameObject.SetActive(true);
        playerAiming.EnableAiming();
    }
}
