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
    public AudioClip[] shootingScaleUpClips;
    public AudioClip[] shootingScaleDownClips;

    private float soundCooldown = 0.5f;  
    private float lastSoundTime;

    public void PlayWalkSound() => PlaySoundsWithDelay(walkClips, walkCoolDown, AudioGroups.Footsteps);

    public void PlayJumpSound() => PlaySoundsWithDelay(jumpClips,0f, AudioGroups.Jump);

    public void PlayLandingSound() => PlaySoundsWithDelay(landingClips, 0f, AudioGroups.Land);

    public void PlayPickupSound() => PlaySoundsWithDelay(pickupClips,0f, AudioGroups.BoxPickup);
    
    public void PlayThrowSound() => PlaySoundsWithDelay(throwClips, 0f, AudioGroups.BoxThrow);

    public void PlayShootingShrinkSound() => PlaySoundsWithDelay(shootingScaleUpClips, 0f, AudioGroups.ShootScaleDown); //we'll need to play with this a bit
    public void PlayShootingGrowSound() => PlaySoundsWithDelay(shootingScaleDownClips, 0f, AudioGroups.ShootScaleUp); //we'll need to play with this a bit

    private void PlaySoundsWithDelay(AudioClip[] audioClips, float coolDown = -1f, AudioGroups group = AudioGroups.Player)
    {
        if (Time.time >= lastSoundTime + (coolDown == -1f ? soundCooldown : coolDown))
        {
            AudioManager.instance.PlayRandomizedSFXs(audioClips, group);
            lastSoundTime = Time.time;
        }
    } 
}
