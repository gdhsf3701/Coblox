using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlayerList : MonoBehaviour
{
    public GameObject Main;
    private int Indx;
    private void Start()
    {
        StartCoroutine(WaitAndPrint(3.5f,Indx));
    }

    IEnumerator WaitAndPrint(float Time,int idx)
    {
        yield return new WaitForSecondsRealtime(Time);
        GameObject Clone = Instantiate(Main, transform);
        Clone.GetComponent<RankingInfo>().No.text = DataBaseScript.Instance.List[idx].Rank.ToString();
        Clone.GetComponent<RankingInfo>().Name.text = DataBaseScript.Instance.List[idx].Name.ToString();
        Clone.GetComponent<RankingInfo>().Point.text = DataBaseScript.Instance.List[idx].Point.ToString();
        try
        {
            ++idx;
            print(DataBaseScript.Instance.List[idx].Name);
            StartCoroutine(WaitAndPrint(1.5f,++Indx));

        }
        catch
        {
            print("인덱스 끝남");
        }

    }
}
