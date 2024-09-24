using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    private VisualElement _startUI;

    private Button _Start;
    private VisualElement _settingUI;

    private Button Setting;

    private Button close;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _Start = root.Q<Button>("Start");
        Setting = root.Q<Button>("Setting");
        close = root.Q<Button>("Close");
        _settingUI = root.Q<VisualElement>("SettingChang");

        _Start.RegisterCallback<ClickEvent>(GameStart);
        Setting.RegisterCallback<ClickEvent>(OpenSetting);
        close.RegisterCallback<ClickEvent>(Close);
    }

    private void GameStart(ClickEvent evt)
    {
        SceneManager.LoadScene("02_LoginorSighUP");
    }
    private void OpenSetting(ClickEvent evt)
    {
        _settingUI.style.display = DisplayStyle.Flex;
    }
    private void Close(ClickEvent evt)
    {
       Application.Quit();
    }
}
