using UnityEngine;
using TMPro;
using System.Collections.Generic;

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
    private string[] soSu = {"�丶�� �ҽ�" , "ġ��" };
    public int[] elementalWant = { 0, 0, 0, 0, 0 };
    public int[] elementalCount = { 0, 0, 0, 0, 0 };
    private bool done = true;
    private bool[] doneCheck = { false , false };
    private bool sosuDone = false;
    [SerializeField] Sprite[] dowSprite;

    private void Awake()
    {
        NowElemental = -1;
        if(elemental != null)
        {
            var munjealist = DataBaseScript.Instance.Munjea;
            
            string k = munjealist[0].N;
            int i = Random.Range(0, elemental.Length);

            switch (munjealist[0].success)
            {
                case "1":
                    elementalWant[i] = int.Parse(munjealist[0].t1);
                    break;
                case "2":
                    elementalWant[i] = int.Parse(munjealist[0].t2);
                    break;
                case "3":
                    elementalWant[i] = int.Parse(munjealist[0].t3);
                    break;
                case "4":
                    elementalWant[i] = int.Parse(munjealist[0].t4);
                    break;
                case "5":
                    elementalWant[i] = int.Parse(munjealist[0].t5);
                    break;

                default:
                    print("����");
                    break;
            }
            text.text = $"{elemental[i]}:{k}";

            //string k = ���� ��;
            //int i = Random.Range(0, elemental.Length);�������� ����� 1 ��
            //int j = ���� ��;
            //elementalWant[i] = j//������ ����;
            //text.text = $"{elemental[i]}:{k}";
        }
        else
        {
            //elemental = new string[2];���� 1���� �丶�� �ҽ� 2���� ġ��ҽ� ���� ������;
            //elementalWant = new int[2]//���� 1���� �丶�� �ҽ� 2���� ġ��ҽ� �� ������;
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
            Cursor.SetCursor(cursorTexture[nowElemental + 1], new Vector2(cursorTexture[nowElemental + 1].width * 0.45f, cursorTexture[nowElemental + 1].height * 0.35f), CursorMode.ForceSoftware);
            if (elemental[4] == null)
            {
                text.text = $"{soSu[nowElemental]}:{elemental[nowElemental]}";
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
