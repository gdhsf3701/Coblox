using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using BackEnd;

public class Bt_Manager : MonoBehaviour
{
    public void Quit_bt_Click()
    {
        Backend.BMember.Logout();
        if (GameObject.Find("ChatManager"))
        {
            Destroy(GameObject.Find("ChatManager"));
        }
        SceneManager.LoadScene("02_LoginorSighUP");
    }
    public void reStart_bt_Click()
    {
        SceneManager.LoadScene(DataBaseScript.Instance.isOwner ? "03_1_Teacher" : "03_2_StudentJoinRoom");//엔드 씬에 오기전 초기화를 실행 해서 문제가 생김 모두 선생님 화면으로 들어가짐
        
    }

   
}
