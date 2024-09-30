using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    private float volume = 0.5f;
    private float bgmVolume = 0.5f;
    private AudioSource _audioSource;

    // 사운드 클립을 저장할 딕셔너리
    public Dictionary<Sound, AudioClip> soundDictionary = new Dictionary<Sound, AudioClip>();

    void Awake()
    {
       DontDestroyOnLoad(gameObject);
        // AudioSource 컴포넌트 가져오기 또는 추가
        _audioSource = gameObject.GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 볼륨 초기화
        _audioSource.volume = volume;

        // 사운드 클립 딕셔너리 초기화
        LoadSounds();
    }

    private void LoadSounds()
    {
        // 각 사운드에 맞는 오디오 클립을 미리 로드합니다.
        soundDictionary[Sound.ButtonClick] = Resources.Load<AudioClip>("Sounds/ButtonClick");
        soundDictionary[Sound.Fail] = Resources.Load<AudioClip>("Sounds/Fail");
        soundDictionary[Sound.Fire] = Resources.Load<AudioClip>("Sounds/Fire");
        soundDictionary[Sound.GameEnd] = Resources.Load<AudioClip>("Sounds/GameEnd");
        soundDictionary[Sound.HawduckClose] = Resources.Load<AudioClip>("Sounds/HawduckClose");
        soundDictionary[Sound.MainMusic] = Resources.Load<AudioClip>("Sounds/MainMusic");
        soundDictionary[Sound.PointUp] = Resources.Load<AudioClip>("Sounds/PointUp");
        soundDictionary[Sound.SauceandCheeseDraw] = Resources.Load<AudioClip>("Sounds/SauceandCheeseDraw");
        soundDictionary[Sound.SauceandCheeseSelect] = Resources.Load<AudioClip>("Sounds/SauceandCheeseSelect");
        soundDictionary[Sound.Success] = Resources.Load<AudioClip>("Sounds/Success");
        soundDictionary[Sound.tiktoksound] = Resources.Load<AudioClip>("Sounds/tiktoksound");
        soundDictionary[Sound.toppingDraw] = Resources.Load<AudioClip>("Sounds/toppingDraw");
        soundDictionary[Sound.toppingSelect] = Resources.Load<AudioClip>("Sounds/toppingSelect");
    }

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        _audioSource.volume = volume; // AudioSource 볼륨 반영
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

    public void PlaySound(Sound sound)
    {
        if (soundDictionary.ContainsKey(sound))
        {
            _audioSource.PlayOneShot(soundDictionary[sound]);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + sound);
        }
    }

    public void ResetSettings()
    {
        SetVolume(0.5f);
        SetBgmVolume(0.5f);
    }
}

public enum Sound
{
    ButtonClick,
    Fail,
    Fire,
    GameEnd,
    HawduckClose,
    MainMusic,
    PointUp,
    SauceandCheeseDraw,
    SauceandCheeseSelect,
    Success,
    tiktoksound,
    toppingDraw,
    toppingSelect
}
