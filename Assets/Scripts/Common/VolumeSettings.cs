using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeSettings : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider mainSlider;
    //public AudioMixer musicMixer;
    //public Slider musicSlider;


    public void Start()
    {
        if (PlayerPrefs.HasKey("MainVolume"))
        {
            LoadMainMusic();
        }
        else { 
            SetMainMusic();
        }
        /*
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadMusic();
        }
        else
        {
            SetMusic();
        }
        */
    }
    /*
    private void LoadMusic()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SetMusic();
    }

    private void SetMusic()
    {
        float volume = musicSlider.value;
        musicMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    */
    private void LoadMainMusic()
    {
        mainSlider.value = PlayerPrefs.GetFloat("MainVolume");
        SetMainMusic();
    }

    public void SetMainMusic()
    {
        float volume = mainSlider.value;
        mainMixer.SetFloat("Main", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MainVolume", volume);
    }
}
