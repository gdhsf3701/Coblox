using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoneChack : MonoBehaviour
{
    
    [SerializeField] Plate plate;
    bool done = true;
    private bool andyunsan = true;
    public void ClickGoopGi()
    {
        for(int i = 0; i < plate.elemental.Length; i++)
        {
            if (plate.elementalWant[i] == plate.elementalCount[i])
            {
                continue;
            }
            else
            {
                done = false;
                break;
            }
        }
        if(done&&andyunsan)
        {
            print("2");
            andyunsan = false;
            GameManager.Instance.NowScore += int.Parse(DataBaseScript.Instance.siteData[DataBaseScript.Instance.site_sunsea,8]);
            ChatManager.Instance.Edit_DataBase_Point(GameManager.Instance.NowScore);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void ClickCooking()
    {
        if (plate.DoneCheck()&&andyunsan)
        {
            print("1");
            print(int.Parse(DataBaseScript.Instance.siteData[DataBaseScript.Instance.site_sunsea, 8]));
            andyunsan = false;
            GameManager.Instance.NowScore += int.Parse(DataBaseScript.Instance.siteData[DataBaseScript.Instance.site_sunsea,8]);
            GameManager.Instance.NowScore += int.Parse(DataBaseScript.Instance.siteData[DataBaseScript.Instance.site_sunsea+1,8]);
            ChatManager.Instance.Edit_DataBase_Point(GameManager.Instance.NowScore);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
