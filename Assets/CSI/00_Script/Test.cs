using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private int NowPoint;
    private int count;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            print("보내기 시작");
            NowPoint += 100;
            global::SendMessage.Instance.SentMessage("GetPoint",$"{NowPoint}");
        }
    }

    public void munjeaSend()
    {
        ++count;
        global::SendMessage.Instance.SentMessage("Munjea",$"{DataBaseScript.Instance.siteData[count,0]}:" +
                                                          $"{DataBaseScript.Instance.siteData[count,1]}:" +
                                                          $"{DataBaseScript.Instance.siteData[count,2]}:" +
                                                          $"{DataBaseScript.Instance.siteData[count,3]}:" +
                                                          $"{DataBaseScript.Instance.siteData[count,4]}:" +
                                                          $"{DataBaseScript.Instance.siteData[count,5]}:" +
                                                          $"{DataBaseScript.Instance.siteData[count,6]}:" +
                                                          $"{DataBaseScript.Instance.siteData[count,7]}");
    }

    private void Start()
    {
        gameObject.transform.position = Vector3.zero;
        //서버에 메새지를 보낼때
        //global::SendMessage.SentMessage("Asd");
    }
}