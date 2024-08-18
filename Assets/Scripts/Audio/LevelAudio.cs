using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudio : MonoBehaviour
{ 
    public AudioClip failClip;
    public AudioClip winClip;  
    public AudioClip sceneMusic;

    public void PlayFailSound() => AudioManager.instance.PlaySFX(failClip, AudioGroups.WinFail);

    public void PlayWinSound() => AudioManager.instance.PlaySFX(winClip, AudioGroups.WinFail);

    public void PlaySceneMusic() => AudioManager.instance.PlayMusic(sceneMusic,AudioGroups.Music);
    
}
