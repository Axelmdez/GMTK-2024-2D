using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float parallaxEffectMultiplier;

    private Transform cameraTransform;
    private Vector3 previousCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
    }

    private void Update()
    {
        Vector3 deltaMovement = cameraTransform.position - previousCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, deltaMovement.y * parallaxEffectMultiplier, 0);
        previousCameraPosition = cameraTransform.position;
    }
}
