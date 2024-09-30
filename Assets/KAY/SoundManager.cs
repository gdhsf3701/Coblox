using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    private float volume = 0.5f;
    private float bgmVolume = 0.5f;

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
    }

    public void SetBgmVolume(float newBgmVolume)
    {
        bgmVolume = Mathf.Clamp01(newBgmVolume);
    }

    public float GetVolume() => volume;
    public float GetBgmVolume() => bgmVolume;

    public void MuteMusic()
    {
        SetBgmVolume(0);
    }

    public void ResetSettings()
    {
        SetVolume(0.5f);
        SetBgmVolume(0.5f);
    }
}

public enum Sound
{

}
