using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudio : MonoBehaviour
{ 
    public AudioClip failClip;
    public AudioClip winClip;  

    public void PlayFailSound() => AudioManager.instance.PlaySFX(failClip);

    public void PlayWinSound() => AudioManager.instance.PlaySFX(winClip);

}
