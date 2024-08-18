using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Vector3 initialPosition;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        
    }

    public void TakeDamage()
    { 
        rb.velocity = Vector2.zero;
        Debug.Log("Player took damage and respawned!");
    }
}