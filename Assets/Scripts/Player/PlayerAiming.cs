using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public Transform gunPivot;  
    public Transform crosshair;
    /// <summary>
    /// may need this later if we have a player sprite we can flip x/y
    /// otherwise we flip the sprite by angles like I do for the gun
    /// it would be -> transform.localScale = new Vector3(1, -1, 1);
    /// </summary>
    [SerializeField] SpriteRenderer playerSpriteRenderer;

    private bool isAiming = true;

    public void DisableAiming() => isAiming = false;
    public void EnableAiming() => isAiming = true;

    void Update()
    {
        if (isAiming)
            AimAtCrosshair();
    }

    void AimAtCrosshair()
    { 
        Vector2 direction = crosshair.position - gunPivot.position;
         
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
         
        gunPivot.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
         
        FlipGunSprite(angle);
    } 

    void FlipGunSprite(float angle)
    {
        if (angle > 90 || angle < -90)
        { 
            gunPivot.transform.localScale = new Vector3(1, -1, 1);
            FlipPlayerSprite(true);
        }
        else
        { 
            gunPivot.transform.localScale = new Vector3(1, 1, 1);
            FlipPlayerSprite(false);
        }

    }
    public void FlipPlayerSprite(bool flip)
    {
        playerSpriteRenderer.flipX = flip;
    }
}
