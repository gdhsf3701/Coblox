using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]float timer;
    int score = 0;
    public int Score 
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
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
        scoreTxt.text = score.ToString();
        DontDestroyOnLoad(Instance);
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        timerTxt.text = Mathf.FloorToInt(timer).ToString();
    }
    private void ScoreChange()
    {
        scoreTxt.text = score.ToString();
    }

    public void TimeOver()
    {
        //총합점수 보내기
        //메인화면으로 이동SceneManager.LoadScene();
    }
}
