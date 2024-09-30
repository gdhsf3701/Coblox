using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEditor;

public class Plate : MonoBehaviour
{
    [SerializeField]private int nowElemental = -1;
    public int NowElemental
    {
        get
        {
            return nowElemental;
        }
        set
        {
            nowElemental = value;
            SetCursor();
        }
    }
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject[] elementals;
    [SerializeField] SpriteRenderer Dow;
    [SerializeField] Camera cam;
    [SerializeField] Collider2D Collider;
    [SerializeField] private Texture2D[] cursorTexture;
    [SerializeField] TMP_InputField inputField;
    public string[] elemental = { "��", "����", "�Ǹ�", "�ø���", "���۷δ�" };
    private string[] soSu = {"토마토소스" , "치즈" };
    public int[] elementalWant = { 0, 0, 0, 0, 0 };
    public int[] elementalCount = { 0, 0, 0, 0, 0 };
    private bool done = true;
    private bool[] doneCheck = { false , false };
    private bool sosuDone = false;
    [SerializeField] Sprite[] dowSprite;

    private void Awake()
    {
        NowElemental = -1;
        var munjealist = DataBaseScript.Instance.siteData;
        int su = DataBaseScript.Instance.site_sunsea;
        string k = munjealist[su, 1];
        int i = Random.Range(su, elemental.Length);
        if (elemental != null)
        {
            

            switch (munjealist[su, 8])
            {
                case "1":
                    elementalWant[i] = int.Parse(munjealist[su, 2]);
                    break;
                case "2":
                    elementalWant[i] = int.Parse(munjealist[su,3]);
                    break;
                case "3":
                    elementalWant[i] = int.Parse(munjealist[su,4]);
                    break;
                case "4":
                    elementalWant[i] = int.Parse(munjealist[su,5]);
                    break;
                case "5":
                    elementalWant[i] = int.Parse(munjealist[su,6]);
                    break;

                default:
                    print("����");
                    break;
            }
            text.text = $"{elemental[i]}:{k}";
            DataBaseScript.Instance.site_sunsea++;
            if(DataBaseScript.Instance.siteData.Length < su)
            {
                Random.Range(0,DataBaseScript.Instance.siteData.Length);
            }

            //string k = 문제 식;
            //int i = Random.Range(0, elemental.Length);여러가지 재료중 1 택
            //int j = 문제 답;
            //elementalWant[i] = j//문제의 정답;
            //text.text = $"{elemental[i]}:{k}";
        }
        else
        {
            elemental = new string[2];
            elemental[1] = munjealist[su,1];
            elemental[2] = munjealist[su+1,1];
            for (int iss = 1; iss < 2; iss++)
            {
                switch (munjealist[su, 8])
                {
                    case "1":
                        elementalWant[iss] = int.Parse(munjealist[iss, 2]);
                        break;        
                    case "2":         
                        elementalWant[iss] = int.Parse(munjealist[iss, 3]);
                        break;        
                    case "3":         
                        elementalWant[iss] = int.Parse(munjealist[iss, 4]);
                        break;        
                    case "4":         
                        elementalWant[iss] = int.Parse(munjealist[iss, 5]);
                        break;        
                    case "5":         
                        elementalWant[iss] = int.Parse(munjealist[iss, 6]);
                        break;

                    default:
                        print("����");
                        break;
                }
            }
            text.text = $"{soSu[0]}:{elemental[0]}";

            //elemental = new string[2];각각 1번은 토마토 소스 2번은 치즈소스 문제 넣으셈;
            //elementalWant = new int[2]//각각 1번은 토마토 소스 2번은 치즈소스 답 넣으셈;
            //elementalCount = new int[2];
            //text.text = $"{soSu[0]}:{elemental[0]}";
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && nowElemental != -1)
        {
            if (Collider != null)
            {
                Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                if (Collider.OverlapPoint(mousePosition)) 
                {
                    GameObject game = Instantiate(elementals[nowElemental], mousePosition, Quaternion.identity);
                    if(game != null) 
                    {
                        elementalCount[nowElemental]++;
                    }
                }
            }
        }
    }
    public bool DoneCheck()
    {
        foreach(var item in doneCheck)
        {
            if (done)
            {
                done = item;
            }
        }
        return done;
    }
    private void SetCursor()
    {
        if (nowElemental >= -1 && nowElemental < cursorTexture.Length - 1)
        {
            if (elemental[0] == "토마토소스")
            {
                Cursor.SetCursor(cursorTexture[nowElemental + 1], new Vector2(cursorTexture[nowElemental + 1].width* 0.49f, cursorTexture[nowElemental + 1].height* 0.49f), CursorMode.ForceSoftware);
                if(nowElemental != -1)
                {
                    text.text = $"{soSu[nowElemental]}:{elemental[nowElemental]}";
                }
                else
                {
                    text.text = $"{soSu[nowElemental+1]}:{elemental[nowElemental+1]}";
                }
            }
            else
            {
                Cursor.SetCursor(cursorTexture[nowElemental + 1], new Vector2(cursorTexture[nowElemental + 1].width*0.45f, cursorTexture[nowElemental + 1].height * 0.35f), CursorMode.ForceSoftware);
            }
        }   
    }
    public void OnInput()
    {
        if(nowElemental == -1)
        {
            return;
        }
        if (elemental[nowElemental].ToString() == inputField.text)
        {
            doneCheck[nowElemental] = true;
            inputField.text = "";
        }
        else
        {
            doneCheck[nowElemental] = false;
            inputField.text = "";
        }
        ChangeDow(nowElemental);
    }

    private void ChangeDow(int nowElemental)
    {
        if (sosuDone)
        {
            Dow.sprite = dowSprite[2];
        }
        else
        {
            Dow.sprite= dowSprite[nowElemental];
        }
    }

}
