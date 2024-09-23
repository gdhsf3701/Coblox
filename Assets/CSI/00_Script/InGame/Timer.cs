using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private Slider slider;
    [SerializeField] private Image FillIm;
    
        
    private float timeRemaining = 10.0f;
    private bool timerIsRunning = false;

    private void Start()
    {
        timeRemaining = DataBaseScript.Instance.Time;
        slider.maxValue = timeRemaining;
        if (timeRemaining > 0)
        {
            timerIsRunning = true;
        }
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                slider.value = timeRemaining;
                if (DataBaseScript.Instance.Time%100 < timeRemaining)
                {
                    FillIm.color = new Color(Mathf.Lerp(0, 242, Time.deltaTime),255,255);
                }else if (DataBaseScript.Instance.Time%50 < timeRemaining)
                {
                    FillIm.color = new Color(242,Mathf.Lerp(255, 188, Time.deltaTime),255);
                }else if (DataBaseScript.Instance.Time%30 < timeRemaining)
                {
                    FillIm.color = new Color(242,Mathf.Lerp(188, 94, Time.deltaTime),255);
                }else if (DataBaseScript.Instance.Time%15 < timeRemaining)
                {
                    FillIm.color = new Color(242,Mathf.Lerp(0, 94, Time.deltaTime),255);
                }
                

                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                TimerEnd();
            }
        }
        

    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        Text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TimerEnd()
    {
        GameObject.Find("InGame_getmessage").GetComponent<InGame_getmessage>().SendEndData();

    }
        
}
