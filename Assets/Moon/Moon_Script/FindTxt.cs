using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FindTxt : MonoBehaviour
{
    TextMeshProUGUI timerTxt;
    TextMeshProUGUI scoreTxt;
    void Awake()
    {
        timerTxt = GameObject.Find("timer").GetComponent<TextMeshProUGUI>();
        scoreTxt = GameObject.Find("scoreTxt").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        GameManager.Instance.scoreTxt = scoreTxt;
        GameManager.Instance.timerTxt = timerTxt;
        GameManager.Instance.NowScore = GameManager.Instance.NowScore;
    }
}
