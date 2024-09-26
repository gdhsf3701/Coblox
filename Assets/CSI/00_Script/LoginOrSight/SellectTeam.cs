using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SellectTeam : MonoBehaviour
{
    [SerializeField] private Enable_UI MainUI;
    
    private void OnEnable()
    {
        MainUI.SetEnableBt(false);
    }

    public void SellectT()
    {
        global::DataBaseScript.Instance.teacher = true;
        SceneManager.LoadScene("03_1_Teacher");

    }

    public void SellectS()
    {
        global::DataBaseScript.Instance.teacher = false;
        SceneManager.LoadScene("03_2_StudentJoinRoom");

    }
}
