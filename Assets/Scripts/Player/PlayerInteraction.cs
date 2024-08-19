using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform armsHoldingPoint;
    public Transform armsAimPoint;

    public float holdDistance = 2.0f;
    public float throwForce = 10f;
    public LayerMask pickupsLayer;

    private Box heldItem;
    private Rigidbody2D rb;
    private Vector2 raycastDirection;
    private float raycastDistance = 1f;
    private PlayerAudio playerAudio;

    private PlayerAiming playerAiming;

    void Start()
    {
        playerAiming = GetComponent<PlayerAiming>();
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
}