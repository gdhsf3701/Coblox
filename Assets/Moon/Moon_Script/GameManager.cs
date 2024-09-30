using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    float timer;
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
    public TextMeshProUGUI timerTxt;
    public TextMeshProUGUI scoreTxt;
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
        DontDestroyOnLoad(gameObject);
        timer = DataBaseScript.Instance.Time;
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
        Cursor.SetCursor(default, new Vector2(0, 0), CursorMode.Auto);
        Destroy(gameObject);
    }
}
