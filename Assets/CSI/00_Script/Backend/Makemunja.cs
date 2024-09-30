using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TexDrawLib;

public class 
    Makemunja : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI 문제;
    [SerializeField]private GameObject _tex;
    [SerializeField]private Button 택1,택2,택3,택4,택5;
    [SerializeField] private AudioClip error, succecClip;
    private AudioSource _audio;
    private string Score;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void SetImg(string text)
    {
        _tex.GetComponent<SetImage>().GetTexture(text);
    }

    private string successSave;
    public void Getnayung(string N, string s, string t1, string t2, string t3, string t4, string t5, string success,string score = "0")
    {
        
        문제.text = N;
        SetImg(s);
        택1.GetComponentInChildren<TextMeshProUGUI>().text = t1;
        택2.GetComponentInChildren<TextMeshProUGUI>().text = t2;
        택3.GetComponentInChildren<TextMeshProUGUI>().text = t3;
        택4.GetComponentInChildren<TextMeshProUGUI>().text = t4;
        택5.GetComponentInChildren<TextMeshProUGUI>().text = t5;
        successSave = success;
        Score = score;
    }

    public void chackjangdap()
    {
        GameObject clickobj = EventSystem.current.currentSelectedGameObject;
        if (successSave == clickobj.name)
        {
            _audio.clip = succecClip;
            _audio.Play();
            clickobj.GetComponent<Image>().DOColor(Color.green, 0.5f).OnComplete(()=> clickobj.GetComponent<Image>().DOColor(Color.white, 0.3f));
            gameObject.transform.DOMoveY(gameObject.transform.position.y + 10, 0.8f, false).SetEase(Ease.OutQuad).OnComplete((() => Destroy(gameObject)));
            DataBaseScript.Instance.NowPoint += int.Parse(Score);

            global::SendMessage.Instance.SentMessage("GetPoint",DataBaseScript.Instance.NowPoint.ToString());
        }
        else
        {
            _audio.clip = error;
            _audio.Play();
            clickobj.transform.DOShakePosition(1, 3f, 20, 90f, true);
            clickobj.GetComponent<Image>().DOColor(Color.red, 0.4f).OnComplete(()=> clickobj.GetComponent<Image>().DOColor(Color.white, 0.2f));
            
        }
    }
}
