using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndPlayerList : MonoBehaviour
{
    public GameObject Main;
    private int Indx;
    private float ShowTime = 1.5f;
    private void Start()
    {
        StartCoroutine(WaitAndPrint(3.5f,Indx));
    }

    IEnumerator WaitAndPrint(float Time,int idx)
    {
        yield return new WaitForSecondsRealtime(Time);
        var List = DataBaseScript.Instance.List[idx];
        GameObject Clone = Instantiate(Main, transform);
        Clone.GetComponent<RankingInfo>().No.text = List.Rank.ToString();
        Clone.GetComponent<RankingInfo>().Name.text = List.Name.ToString();
        Clone.GetComponent<RankingInfo>().Point.text = List.Point.ToString();
        if (List.Name == DataBaseScript.Instance.NicName)
        {
            Clone.GetComponent<Image>().color = new Color(1, 0.9994608f, 0.8066038f);
            print("자신의 정보");
            if (GameObject.Find("MyRankShow") != null) GameObject.Find("MyRankShow").GetComponent<TextMeshProUGUI>().text = $"나의 순위 : {List.Rank}";
            else
            {
                print("없어");
            }
        }   
        try
        {
            ++idx;
            print(DataBaseScript.Instance.List[idx].Name);
            ShowTime -= 0.2f;
            if (ShowTime < 0)
            {
                ShowTime = 0f;
            }
            StartCoroutine(WaitAndPrint(ShowTime,++Indx));

        }
        catch
        {
            print("인덱스 끝남");
        }

    }
}
