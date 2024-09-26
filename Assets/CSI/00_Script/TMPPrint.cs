using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TMPPrint : MonoBehaviour
{
    private TextMeshProUGUI TMP;
    private string MEG;
    public float Wait_Time;
    public float TypingTime;

    private void Awake()
    {
        TMP = GetComponent<TextMeshProUGUI>();
        MEG = TMP.text;
    }

    private void OnEnable()
    {
        TMP.gameObject.SetActive(true);
        TMP.text = "";

        StartCoroutine(Wait(TypingTime));
    }

    IEnumerator Wait(float TypingTime)
    {
        yield return new WaitForSecondsRealtime(Wait_Time);
        TMP.DOText(MEG, TypingTime);
    }

    private void OnDisable()
    {
        TMP.gameObject.SetActive(false);
    }
}
