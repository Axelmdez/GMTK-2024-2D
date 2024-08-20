using UnityEngine;

public class PlayerShooting2 : MonoBehaviour
{
    public Transform firePoint;
    public GameObject shrinkProjectilePrefab;
    public GameObject growProjectilePrefab;
    public float projectileSpeed = 10f;
    private PlayerAudio playerAudio;
    private PlayerInteraction playerInteraction;

    private void Awake()
    {
        playerInteraction = GetComponent<PlayerInteraction>();
        playerAudio = GetComponent<PlayerAudio>();
    }

    void Update()
    {
        if (playerInteraction.HoldingItem()) return;
    
        HandleShooting();
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0)) //grow
        {
            Shoot(growProjectilePrefab);
            playerAudio.PlayShootingGrowSound();
        }
        else if (Input.GetMouseButtonDown(1)) //shrink
        {
            Shoot(shrinkProjectilePrefab);
            playerAudio.PlayShootingShrinkSound();
        }
    }

    void Shoot(GameObject projectilePrefab)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)firePoint.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        rb.velocity = direction * projectileSpeed; 
    }
}
