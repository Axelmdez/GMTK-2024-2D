using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] menuTaps;
    [SerializeField] private AudioClip menuError;


    public void PlayMenuTaps() => AudioManager.instance.PlayRandomizedSFXs(menuTaps);

    public void PlayMenuError() => AudioManager.instance.PlaySFX(menuError);
}
