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
        SoundManager.Instance.PlaySound(Sound.ButtonClick);
        for (int i = 0; i < plate.elemental.Length; i++)
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
            SoundManager.Instance.PlaySound(Sound.Success);
            GameManager.Instance.NowScore += int.Parse(DataBaseScript.Instance.siteData[DataBaseScript.Instance.site_sunsea,8]);
            ChatManager.Instance.Edit_DataBase_Point(GameManager.Instance.NowScore);
        }
        else
        {
            SoundManager.Instance.PlaySound(Sound.Fail);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void ClickCooking()
    {
        SoundManager.Instance.PlaySound(Sound.ButtonClick);
        if (plate.DoneCheck()&&andyunsan)
        {
            SoundManager.Instance.PlaySound(Sound.Success);
            andyunsan = false;
            GameManager.Instance.NowScore += int.Parse(DataBaseScript.Instance.siteData[DataBaseScript.Instance.site_sunsea,8]);
            GameManager.Instance.NowScore += int.Parse(DataBaseScript.Instance.siteData[DataBaseScript.Instance.site_sunsea+1,8]);
            ChatManager.Instance.Edit_DataBase_Point(GameManager.Instance.NowScore);
        }
        else
        {
            SoundManager.Instance.PlaySound(Sound.Fail);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
