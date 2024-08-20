using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum AudioGroups
{
    Main,
    Music,
    Ambience,
    Environment,
    Button,
    DoorOpen,
    DoorClose,
    BoxHitSurface,
    Menu,
    WinFail,
    Click,
    Error,
    Player,
    Footsteps,
    Jump,
    Land,
    ShootScaleUp,
    ShootScaleDown,
    BoxPickup,
    BoxThrow,
    BoxScaleDown,
    BoxScaleUp,
    LaserHitSurface
}


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; 

    public Dictionary<AudioGroups, AudioSource> audioSources = new Dictionary<AudioGroups, AudioSource>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SetupAudioSources();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetupAudioSources()
    {
        foreach (Transform child in transform)
        {
            AudioSource source = child.GetComponent<AudioSource>();
            if (source != null)
            {
                AudioGroups soundType;
                if (System.Enum.TryParse(child.name, out soundType))
                {
                    audioSources.Add(soundType, source);
                }
                else
                {
                    Debug.LogWarning("No matching enum for sound: " + child.name);
                }
            }
        }
    }

    public void PlayMusic(AudioClip clip, AudioGroups group)
    {
        audioSources[group].clip = clip;
        audioSources[group].Play(); 
    }

    public void PlayRandomizedSFXs(AudioClip[] clips, AudioGroups group)
    {
        audioSources[group].PlayOneShot(clips[Random.Range(0, clips.Length - 1)]);
    }

    public void PlaySFX(AudioClip clip, AudioGroups group) 
    {
        audioSources[group].clip = clip;
        audioSources[group].PlayOneShot(clip);
    }
    public void StopGroup(AudioGroups group) => audioSources[group].Stop(); 

}