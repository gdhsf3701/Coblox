using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    private float volume = 0;
    private float bgmvolume = 0;

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        // 볼륨에 대한 실제 로직 적용 (예: AudioSource의 volume 설정 등)
    }

    public void SetBgmVolume(float newVolume)
    {
        bgmvolume = Mathf.Clamp01(newVolume);
        // BGM에 대한 실제 로직 적용 (예: BGM AudioSource의 volume 설정 등)
    }

    public float GetVolume()
    {
        return volume;
    }

    public float GetBgmVolume()
    {
        return bgmvolume;
    }
}

public enum Sound
{

}
