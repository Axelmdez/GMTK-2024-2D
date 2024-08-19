using System;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip[] walkClips;
    public float walkCoolDown = .5f;
    public AudioClip[] jumpClips; 
    public AudioClip[] landingClips;
    public AudioClip[] pickupClips;
    public AudioClip[] throwClips;
    public AudioClip[] shootingClips; 

    private float soundCooldown = 0.5f;  
    private float lastSoundTime;

    public void PlayWalkSound() => PlaySoundsWithDelay(walkClips, walkCoolDown);

    public void PlayJumpSound() => PlaySoundsWithDelay(jumpClips,0f);

    public void PlayLandingSound() => PlaySoundsWithDelay(landingClips, 0f);

    public void PlayPickupSound() => PlaySoundsWithDelay(pickupClips,0f);
    
    public void PlayThrowSound() => PlaySoundsWithDelay(throwClips, 0f);

    public void PlayShootingSound() => PlaySoundsWithDelay(shootingClips, 0f); //we'll need to play with this a bit

    private void PlaySoundsWithDelay(AudioClip[] audioClips, float coolDown = -1f)
    {
        if (Time.time >= lastSoundTime + (coolDown == -1f ? soundCooldown : coolDown))
        {
            AudioManager.instance.PlayRandomizedSFXs(audioClips, AudioGroups.Player);
            lastSoundTime = Time.time;
        }
    } 
}
