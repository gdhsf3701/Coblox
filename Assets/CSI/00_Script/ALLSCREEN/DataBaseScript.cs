using System.Collections;
using System.Collections.Generic;
using BackEnd.Functions;
using UnityEngine;

public class DataBaseScript : MonoBehaviour
{
    public static DataBaseScript Instance;
    public GameObject AdminImage,group;
    public bool isOwner;
    public string UID;
    public string NicName;
    public string[ , ] siteData;
    public float Time;
    public int NowPoint;
    public List<(string N, string s, string t1, string t2, string t3, string t4, string t5, string success,string score)> Munjea;
    //[]갯수 , [][]정보
    //** [이름,점수] **//
    public List<(int Rank, string Name, int Point)> List = new List<(int Rank, string Name, int Point)>();

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void Resetvalue()
    {
        teacher = false;
        inGuild = false;
        isOwner = false;
        siteData = null;
        List = null;
        NowPoint = 0;

    }
    
    //--------------------------------데이터
    public bool teacher;
    public bool Admin;
    public bool inGuild{
        get
        {
            return Admin;
        }
        set
        {
            Admin = value;
            AdminImage.SetActive(value);
        }
    }


    
}
