using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    private VisualElement _startUI;
    private VisualElement _setting;
    private Button _Start;

    private Button Setting;

    private Button close;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _Start = root.Q<Button>("Start");
        Setting = root.Q<Button>("Setting");
        close = root.Q<Button>("Close");

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
        Setting.style.display = DisplayStyle.Flex;
    }
    private void Close(ClickEvent evt)
    {
       Application.Quit();
    }
}
