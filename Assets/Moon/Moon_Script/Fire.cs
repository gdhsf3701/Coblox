using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Fire : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] MoveAndDrop pizza;
    [SerializeField] GameObject game;
    private bool andyunsan = true;
    private float lastTime;
    int firstRand = 0;
    bool timeCheck = false;
    private void Awake()
    {
        pizza.OnFireEnter += CanTimeCheck;
        pizza.OnFireExit += CantTimeCheck;
    }
    private void Start()
    {
        RandomFireTime();
        Cursor.SetCursor(default,new Vector2(0,0),CursorMode.Auto);
    }
    private void Update()
    {
        if (timeCheck) 
        {
            lastTime -= Time.deltaTime;
            ChangeTmpText();
            ChagneTextAColor();    
        }
    }
    private void CanTimeCheck()
    {
        timeCheck = true;
    }
    private void CantTimeCheck()
    {
        timeCheck = false;
    }
    private void RandomFireTime()
    {
        firstRand = Random.Range(5,11);
        lastTime = firstRand;
        text.text = lastTime.ToString("F2");
    }
    private void ChangeTmpText()
    {
        text.text = lastTime.ToString("F2");
    }
    private void ChagneTextAColor()
    {
        Color textColor = text.color;
        float lerpValue = Mathf.Clamp01((firstRand - lastTime) / (firstRand - 3f));
        textColor.a = Mathf.Lerp(1, 0, lerpValue);
        text.color = textColor;
    }
    public void DoneCheck()
    {
        if(lastTime <= 0.5f && lastTime >= 0f && andyunsan)
        {
            andyunsan = false;
            GameManager.Instance.NowScore += Random.Range(1,5);
        }
        //총합점수 보내기
        ChatManager.Instance.Edit_DataBase_Point(GameManager.Instance.NowScore);



        SceneManager.LoadScene("RestaurantStart");
        //메인화면으로 이동SceneManager.LoadScene();
    } 
}
