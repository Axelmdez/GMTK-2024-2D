using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAudio : MonoBehaviour
{
    public AudioClip[] hitSurfaceClips;
    public AudioClip[] scalingClips;  

    private float soundCooldown = 0.5f;  
    private float lastSoundTime;

    public void PlayHitSound() => PlaySoundsWithDelay(hitSurfaceClips);

    public void PlayScalingSound(int index) => AudioManager.instance.PlaySFX(scalingClips[index]);

    private void PlaySoundsWithDelay(AudioClip[] walkClips, float coolDown = -1f)
    {
        if (Time.time >= lastSoundTime + (coolDown == -1f ? soundCooldown : coolDown))
        {
            AudioManager.instance.PlayRandomizedSFXs(walkClips);
            lastSoundTime = Time.time;
        }
    }
}
