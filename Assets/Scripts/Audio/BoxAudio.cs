using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAudio : MonoBehaviour
{
    public AudioClip[] hitSurfaceClips;
    public AudioClip scalingUp;
    public AudioClip scalingDown;

    private float soundCooldown = 0.5f;  
    private float lastSoundTime;

    public void PlayHitSound() => PlaySoundsWithDelay(hitSurfaceClips);

    public void PlayScalingSoundDown() => AudioManager.instance.PlaySFX(scalingDown, AudioGroups.BoxScaleDown);
    public void PlayScalingSoundUp() => AudioManager.instance.PlaySFX(scalingUp, AudioGroups.BoxScaleUp);

    private void PlaySoundsWithDelay(AudioClip[] audioClips, float coolDown = -1f)
    {
        if (Time.time >= lastSoundTime + (coolDown == -1f ? soundCooldown : coolDown))
        {
            AudioManager.instance.PlayRandomizedSFXs(audioClips, AudioGroups.BoxHitSurface);
            lastSoundTime = Time.time;
        }
    }
}
