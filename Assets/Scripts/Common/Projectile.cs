using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isShrinkProjectile;

    [SerializeField] AudioClip[] shootingClips;
    [SerializeField] AudioClip[] hitSurfaceClips;

    private void Awake()
    { 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Box box = collision.collider.GetComponent<Box>();
        if (box != null)
        {
            if (isShrinkProjectile)
            {
                box.BoxTransform(box.transform, true);
            }
            else
            {
                box.BoxTransform(box.transform, false);
            } 

        }
        else
        {
            //play hit surface
        }

        Destroy(gameObject);

    }
}
