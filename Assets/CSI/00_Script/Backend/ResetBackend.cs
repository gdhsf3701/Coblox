using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
// 뒤끝 SDK namespace 추가
using BackEnd;
using TexDrawLib;
using TMPro;
using Unity.VisualScripting;

public class BackendManager : MonoBehaviour
{
    [SerializeField]private GameObject group;
    [SerializeField] private TMP_InputField _tmpInputField;
    int i = 0;
    private int j = 0;



    public string GetDataTable(string fileId) {
        // 파일 가져오기
        Debug.Log($"{fileId}의 데이터를 불러옵니다.");
        try
        {
            int.Parse(fileId);

        }
        catch 
        {
            print("에러");
            return $"{fileId}의 값을 다시 한번 확인해 주세요";
        }
        //var bro = Backend.Chart.GetChartListByFolderV2(int.Parse(fileId));
        var bro = Backend.Chart.GetChartContents(fileId);
        if(bro.IsSuccess() == false) {
            //Debug.LogError($"{fileId}의 데이터를 불러오는 중, 에러가 발생했습니다. : " + bro);
            return $"{fileId}의 값을 다시 한번 확인해 주세요";
        }

        Debug.Log("데이터 불러오기에 성공했습니다. : " + bro);
        
        DataBaseScript.Instance.siteData = new string[bro.FlattenRows().Count, bro.FlattenRows()[0].Count];
        i = 0;
        j = 0;
        foreach(LitJson.JsonData row in bro.FlattenRows())
        {
            DataBaseScript.Instance.siteData[i,0] = row["Name"].ToString();
            DataBaseScript.Instance.siteData[i,1] = row["susick"].ToString();
            DataBaseScript.Instance.siteData[i,2] = row["tack1"].ToString();
            DataBaseScript.Instance.siteData[i,3] = row["tack2"].ToString();
            DataBaseScript.Instance.siteData[i,4] = row["tack3"].ToString();
            DataBaseScript.Instance.siteData[i,5] = row["tack4"].ToString();
            DataBaseScript.Instance.siteData[i,6] = row["tack5"].ToString();
            DataBaseScript.Instance.siteData[i,7] = row["success"].ToString();
            DataBaseScript.Instance.siteData[i,8] = row["score"].ToString();/////////////////////////////////////
            i++;
        }
        return "불러오기 성공";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (j >= i)
            {
                print("차트가 비어있습니다.");
                return;
            }
            GameObject obj = Instantiate(group, transform);
            obj.GetComponent<Makemunja>().Getnayung(DataBaseScript.Instance.siteData[j,0], DataBaseScript.Instance.siteData[j,1], DataBaseScript.Instance.siteData[j,2], DataBaseScript.Instance.siteData[j,3], DataBaseScript.Instance.siteData[j,4], DataBaseScript.Instance.siteData[j,5], DataBaseScript.Instance.siteData[j,6],DataBaseScript.Instance.siteData[j,7]);
            j++;
        }
    }

    public void LoadFile()
    {
        _tmpInputField.text = GetDataTable(_tmpInputField.text.ToString());
    }
}
