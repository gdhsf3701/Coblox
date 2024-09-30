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
        // ������ ���� ���� ���� ���� (��: AudioSource�� volume ���� ��)
    }

    public void SetBgmVolume(float newVolume)
    {
        bgmvolume = Mathf.Clamp01(newVolume);
        // BGM�� ���� ���� ���� ���� (��: BGM AudioSource�� volume ���� ��)
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
