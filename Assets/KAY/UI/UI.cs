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
    private Button closeSetting;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _Start = root.Q<Button>("Start");
        Setting = root.Q<Button>("Setting");
        close = root.Q<Button>("Close");
        _startUI = root.Q<VisualElement>("StartScene");
        _settingUI = root.Q<VisualElement>("SettingChang");
        closeSetting = root.Q<Button>("CloseSetting");

        _Start.RegisterCallback<ClickEvent>(GameStart);
        Setting.RegisterCallback<ClickEvent>(OpenSetting);
        close.RegisterCallback<ClickEvent>(Close);
        _settingUI.style.display = DisplayStyle.None;
        _startUI.style.display = DisplayStyle.Flex;
        closeSetting.RegisterCallback<ClickEvent>(CloseSetting);
    }
    private void GameStart(ClickEvent evt)
    {
        SceneManager.LoadScene("02_LoginorSighUP");
    }
    private void OpenSetting(ClickEvent evt)
    {
        _settingUI.style.display = DisplayStyle.Flex;
        _startUI.style.display = DisplayStyle.None;
    }
    private void CloseSetting(ClickEvent evt)
    {
        _settingUI.style.display = DisplayStyle.None;
        _startUI.style.display = DisplayStyle.Flex;
    }
    private void Close(ClickEvent evt)
    {
       Application.Quit();
    }
}
