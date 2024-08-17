using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{ 
    public static AudioManager instance;
    
    public AudioSource audioSource;
    public AudioSource audioEffect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        audioEffect.clip = clip;
        audioEffect.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void StopEffect()
    {
        audioEffect.Stop();
    }
}
