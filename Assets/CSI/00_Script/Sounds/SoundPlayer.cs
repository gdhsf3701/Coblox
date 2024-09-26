using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour, Ipoolable
{
    [SerializeField] private AudioMixerGroup _sfxGroup, _musicGroup;
    [SerializeField] private string _poolName;
    public string PoolName => _poolName;
    public GameObject ObjectPrefab => gameObject;


    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundSO data)
    {
        if (data.audioType == AudioType.SFX)
        {
            _audioSource.outputAudioMixerGroup = _sfxGroup;
        }
        else if (data.audioType == AudioType.Music)
        {
            _audioSource.outputAudioMixerGroup = _musicGroup;
        }

        _audioSource.volume = data.volume;
        _audioSource.pitch = data.basePitch;
        if (data.randomizePitch)
        {
            _audioSource.pitch += Random.Range(-data.randomPicthModifier, data.randomPicthModifier);
        }

        _audioSource.clip = data.clip;
        _audioSource.loop = data.loop;

        if (!data.loop)
        {
            float time = _audioSource.clip.length + 0.2f;
            DOVirtual.DelayedCall(time, () => PoolManager.Instance.Push(this));
        }
        _audioSource.Play();
    }

    public void StopAndGoToPool()
    {
        _audioSource.Stop();
        PoolManager.Instance.Push(this);
    }

    public void ResetItem()
    {
    }
}
