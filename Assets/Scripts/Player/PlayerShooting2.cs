using UnityEngine;

public class PlayerShooting2 : MonoBehaviour
{
    public Transform firePoint;
    public GameObject shrinkProjectilePrefab;
    public GameObject growProjectilePrefab;
    public float projectileSpeed = 10f;

    void Update()
    {
        HandleShooting();
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click for shrink
        {
            Shoot(shrinkProjectilePrefab);
        }
        else if (Input.GetMouseButtonDown(1)) // Right-click for growth
        {
            Shoot(growProjectilePrefab);
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
