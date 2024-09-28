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
            andyunsan = false;
            GameManager.Instance.Score += 300;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void ClickCooking()
    {
        if (plate.DoneCheck()&&andyunsan)
        {
            andyunsan = false;
            GameManager.Instance.Score += 400;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void TimeOver()
    {
        //총합점수 보내기
        //메인화면으로 이동SceneManager.LoadScene();
    }
}
