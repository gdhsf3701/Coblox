using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.SceneManagement;

public class Enable_UI : MonoBehaviour
{
    [SerializeField] private GameObject login, signup,Main;
    [SerializeField]private Login login_Script;
    [SerializeField] private sighup _sighup;

    private void Awake()
    {
        DataBaseScript.Instance.Resetvalue();

    }

    public void Open_LoginPopup()
    {
        Main.SetActive(false);
        login.SetActive(true);
    }
    public void Open_sigupPopup()
    {
        Main.SetActive(false);

        signup.SetActive(true);
    }

    public void returnmain()
    {
        DataBaseScript.Instance.Resetvalue();

        login_Script.resetUI();
        _sighup.resetUI();
        Main.SetActive(true);
        login.SetActive(false);
        signup.SetActive(false);
        if (Backend.IsLogin)
        {
            Backend.BMember.Logout();
        }

    }

    public void returnstartscreen()
    {
        SceneManager.LoadScene("01_Start");


    }
}
