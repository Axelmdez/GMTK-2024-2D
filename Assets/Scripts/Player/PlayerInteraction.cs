using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform holdPoint;
    public float holdDistance = 2.0f;
    public float throwForce = 10f;
    public LayerMask pickupsLayer;

    private Box heldItem;
    private Rigidbody2D rb;
    private Vector2 raycastDirection;
    private float raycastDistance = 1f;
    private PlayerAudio playerAudio;
    public static event Action tryExit;
    public static event Action boxLifted;
    void Start()
    {
        playerAudio = GetComponent<PlayerAudio>();
    }
    void Update()
    {
        HandleInteraction();
    }

    void HandleInteraction()
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
                    Debug.Log($"Held Item Mass: {heldRb.mass}, Drag: {heldRb.drag}");
                    holdPoint.position += Vector3.up * holdDistance;
                    heldItem.transform.position = holdPoint.position;
                    heldItem.transform.parent = transform;
                }
            }
            else
            {
                // Throw the box
                playerAudio.PlayThrowSound();
                heldItem.transform.parent = null;
                Rigidbody2D heldRb = heldItem.GetComponent<Rigidbody2D>();
                Debug.Log($"Held Item Mass: {heldRb.mass}, Drag: {heldRb.drag}");
                heldRb.isKinematic = false;
                heldRb.velocity = new Vector2(transform.localScale.x * (((int)heldItem.boxType) < 1 ? throwForce : throwForce / 3), 0);
                heldItem = null;
                holdPoint.position -= Vector3.up * holdDistance;
            }

            tryExit?.Invoke();  
            boxLifted?.Invoke();
        }
    }

    public bool HoldingItem()
    {
        return heldItem != null;
    }
}