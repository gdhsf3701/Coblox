using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]float timer;
    int _nowScore = 0;

    public int NowScore 
    {
        get
        {
            return _nowScore;
        }
        set
        {
            _nowScore = value;
            DataBaseScript.Instance.NowPoint = value;
            ScoreChange();
        }
    }
    TextMeshProUGUI timerTxt;
    TextMeshProUGUI scoreTxt;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        timerTxt = GameObject.Find("timer").GetComponent<TextMeshProUGUI>();
        scoreTxt = GameObject.Find("scoreTxt").GetComponent<TextMeshProUGUI>();
        scoreTxt.text = _nowScore.ToString();
        DontDestroyOnLoad(Instance);
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            TimeOver();
        }
        timerTxt.text = Mathf.FloorToInt(timer).ToString();
    }
    private void ScoreChange()
    {
        scoreTxt.text = _nowScore.ToString();
    }

    public void TimeOver()
    {
        //총합점수 보내기  ---- Done.

        ChatManager.Instance.Edit_DataBase_Point(NowScore);
        Destroy(gameObject);
        //메인화면으로 이동SceneManager.LoadScene();
    }
}
