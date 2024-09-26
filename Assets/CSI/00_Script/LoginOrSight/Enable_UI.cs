using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enable_UI : MonoBehaviour
{
    [SerializeField] private GameObject login, signup,Main;
    [SerializeField]private Login login_Script;
    [SerializeField] private sighup _sighup;
    private Button[] loginandSightbt;

    private void Awake()
    {
        DataBaseScript.Instance.Resetvalue();
        loginandSightbt = Main.GetComponentsInChildren<Button>();
    }

    public void Open_LoginPopup()
    {
        //Main.SetActive(false);
        SetEnableBt(false);
        login.SetActive(true);
    }
    public void Open_sigupPopup()
    {
        //Main.SetActive(false);
        SetEnableBt(false);

        signup.SetActive(true);
    }

    public void SetEnableBt(bool Enable)
    {
        foreach (var Bt in loginandSightbt)
        {
            Bt.interactable = Enable;
        }
    }
    public void returnMain()
    {
        DataBaseScript.Instance.Resetvalue();

        login_Script.resetUI();
        _sighup.resetUI();
        //Main.SetActive(true);
        SetEnableBt(true);

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
