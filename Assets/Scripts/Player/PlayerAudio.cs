using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip walkClip;
    public AudioClip jumpClip;
    public AudioClip pickupClip;
    public AudioClip throwClip;
    public AudioClip pushClip;

    public void PlayWalkSound() => AudioManager.instance.PlayRandomizedSFX(walkClip);
    
    public void PlayJumpSound() => AudioManager.instance.PlaySFX(jumpClip);
    
    public void PlayPickupSound() => AudioManager.instance.PlaySFX(pickupClip);
    
    public void PlayThrowSound() => AudioManager.instance.PlaySFX(throwClip);
    
    public void PlayPushSound() => AudioManager.instance.PlaySFX(pushClip);

    // Example triggers based on player actions
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            PlayJumpSound();
        }

        if (Input.GetButtonDown("Pickup"))
        {
            PlayPickupSound();
        }

        if (Input.GetButtonDown("Throw"))
        {
            PlayThrowSound();
        }

        if (Input.GetButton("Push"))
        {
            PlayPushSound();
        }

        if (Input.GetButton("Walk"))
        {
            PlayWalkSound();
        }
    }
}
