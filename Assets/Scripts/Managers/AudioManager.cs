using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource ambienceSource;
    public AudioSource reverbSource;
    public AudioSource menuSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayRandomizedSFXs(AudioClip[] clips) => sfxSource.PlayOneShot(clips[Random.Range(0, clips.Length-1)]); 

    public void PlaySFX(AudioClip clip) 
    {
        sfxSource.clip = clip;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
    public void PlayAmbience(AudioClip clip)
    {
        ambienceSource.clip = clip;
        ambienceSource.Play();
    }
    public void PlayReverb(AudioClip clip)
    {
        reverbSource.clip = clip;
        reverbSource.Play();
    }

    public void StopReverb() => reverbSource.Stop();
    public void StopAmbience() => ambienceSource.Stop();
    public void StopMusic() => musicSource.Stop();
    public void StopSFX() => sfxSource.Stop();

}