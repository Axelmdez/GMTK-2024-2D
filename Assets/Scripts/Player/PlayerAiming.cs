using UnityEngine;

public class CharacterAiming : MonoBehaviour
{
    public Transform riflePivot; // The point where the rifle rotates
    public Transform crosshair;  // The crosshair object

    /// <summary>
    /// may need this later if we have a player sprite we can flip x/y
    /// otherwise we flip the sprite by angles like I do for the gun
    /// it would be -> transform.localScale = new Vector3(1, -1, 1);
    /// </summary>
    [SerializeField] SpriteRenderer playerSpriteRenderer;

    void Update()
    {
        AimAtCrosshair();
    }

    void AimAtCrosshair()
    { 
        Vector2 direction = crosshair.position - riflePivot.position;
         
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
         
        riflePivot.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
         
        FlipCharacterSprite(angle);
    }

    void FlipCharacterSprite(float angle)
    {
        if (angle > 90 || angle < -90)
        { 
            riflePivot.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        { 
            riflePivot.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
