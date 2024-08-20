using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Camera cam;
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public LayerMask pickupsLayer;
    public float laserLength = 100;
    public float laserLerpSpeed = 5.0f;
    public float laserMaxAngle = 100.0f;
    public float boxTransformTime = 2.0f;
    public SpriteRenderer player_sprite;

    private bool facingLeft;
    private Vector2 laserTargetDirection;
    private bool isTiming;
    private float timer;
    private Transform hitTransform;
    private bool shrinkMode;

    private PlayerInteraction playerInteraction;
    private PlayerAudio playerAudio;

    void Start()
    {
        playerAudio = GetComponent<PlayerAudio>();
        lineRenderer.enabled = false;
        facingLeft = false;
        isTiming = false;
        shrinkMode = false;
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    void Update()
    { 
        UpdateGunMode();
        Shoot();
    }

    void UpdateGunMode()
    {
        if (Input.GetMouseButtonDown(1))
        {
            shrinkMode = !shrinkMode;
        }
    }

    void Shoot()
    {   if (!playerInteraction.HoldingItem()) {
            if (Input.GetMouseButtonDown(0))
            {
                playerAudio.PlayShootingGrowSound(); 
                EnableLaser();
            }

            if (Input.GetMouseButton(0))
            {
                UpdateLaser();
            }

            if (Input.GetMouseButtonUp(0))
            {
                DisableLaser();
            }
        } 
    }

    private void EnableLaser()
    {
        lineRenderer.enabled = true;
    }

    private void UpdateLaser()
    {
        var mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, mousePos);

        Vector2 direction = mousePos - (Vector2)firePoint.position;
        direction.Normalize();
        if (player_sprite.flipX)
        {
            facingLeft = true;
        }
        else { 
            facingLeft = false;
        }
        Vector2 characterDirection = facingLeft ? Vector2.left : Vector2.right;
        float angle = Vector2.Angle(characterDirection, direction);

        if (angle > laserMaxAngle)
        {
            float sign = Mathf.Sign(Vector2.SignedAngle(characterDirection, direction));
            Quaternion rotation = Quaternion.AngleAxis(laserMaxAngle * sign, Vector3.forward);
            direction = rotation * characterDirection;
        }

        laserTargetDirection = Vector2.Lerp(laserTargetDirection, direction, laserLerpSpeed * Time.deltaTime);
        Vector2 laserEndPoint = (Vector2)firePoint.position + laserTargetDirection * laserLength;
        lineRenderer.SetPosition(1, laserEndPoint);
        /*
        float angle = Vector2.SignedAngle(characterDirection, direction);
        if (angle > laserMaxAngle)
        {   
            float sign = Mathf.Sign(Vector2.SignedAngle(characterDirection, direction));
            Quaternion rotation = Quaternion.AngleAxis(laserMaxAngle * sign, Vector3.forward);
            direction = rotation * characterDirection;
        }

        laserTargetDirection = Vector2.Lerp(laserTargetDirection, direction, laserLerpSpeed * Time.deltaTime);
        Vector2 laserEndPoint = (Vector2)firePoint.position + laserTargetDirection * laserLength;
        lineRenderer.SetPosition(1, laserEndPoint);
        */
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, laserTargetDirection, laserLength, pickupsLayer);
        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);

            if (isTiming && hit.collider.transform == hitTransform)
            {
                timer += Time.deltaTime;

                if (timer >= boxTransformTime)
                {
                    hit.transform.GetComponent<Box>().BoxTransform(hitTransform, shrinkMode);
                    isTiming = false;
                    timer = 0.0f;
                }
            }
            else
            {
                isTiming = true;
                timer = 0.0f;
                hitTransform = hit.collider.transform;
            }
        }
        else
        {
            isTiming = false;
            timer = 0.0f;
        }
    }

    private void DisableLaser()
    {
        lineRenderer.enabled = false;
    }
}
