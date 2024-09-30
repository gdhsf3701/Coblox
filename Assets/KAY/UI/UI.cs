using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    private VisualElement _startUI;    // ���� ȭ�� UI
    private VisualElement _settingUI;  // ���� UI

    private Button _Start;      // ���� ���� ��ư
    private Button Setting;     // ���� ��ư
    private Button close;       // ���� ���� ��ư
    private Button closeSetting; // ���� �ݱ� ��ư

    private Slider bgmSlider;   // BGM ���� �����̴�
    private Slider soundSlider; // ���� ���� �����̴�

    private void Start()
    {
        // UI �����κ��� VisualElement ��������
        var root = GetComponent<UIDocument>().rootVisualElement;

        // ��ư ��ҵ� ��������
        _Start = root.Q<Button>("Start");
        Setting = root.Q<Button>("Setting");
        close = root.Q<Button>("Close");
        closeSetting = root.Q<Button>("CloseSetting");

        // �����̴� ��ҵ� ��������
        bgmSlider = root.Q<Slider>("BGMSlider");
        soundSlider = root.Q<Slider>("SoundSlider");

        // ȭ�� UI ��ҵ� ��������
        _startUI = root.Q<VisualElement>("StartScene");
        _settingUI = root.Q<VisualElement>("SettingChang");

        // �̺�Ʈ ������ ���
        _Start.RegisterCallback<ClickEvent>(GameStart);
        Setting.RegisterCallback<ClickEvent>(OpenSetting);
        close.RegisterCallback<ClickEvent>(Close);
        closeSetting.RegisterCallback<ClickEvent>(CloseSetting);

        // �����̴� �� ���� �� ���� ����
        bgmSlider.RegisterValueChangedCallback(evt =>
        {
            SoundManager.Instance.SetBgmVolume(evt.newValue / 100f); // BGM ���� ����
        });

        soundSlider.RegisterValueChangedCallback(evt =>
        {
            SoundManager.Instance.SetVolume(evt.newValue / 100f); // ȿ���� ���� ����
        });

        // �⺻ ���� ����: ���� ȭ���� �����ְ�, ���� ȭ�� ����
        _settingUI.style.display = DisplayStyle.None;
        _startUI.style.display = DisplayStyle.Flex;
    }

    // ���� ���� �� ȣ��
    private void GameStart(ClickEvent evt)
    {
        SoundManager.Instance.PlaySound(Sound.ButtonClick);
        SceneManager.LoadScene("02_LoginorSighUP");
    }

    // ���� UI ����
    private void OpenSetting(ClickEvent evt)
    {
        SoundManager.Instance.PlaySound(Sound.ButtonClick);
        _settingUI.style.display = DisplayStyle.Flex;
        _startUI.style.display = DisplayStyle.None;
       
    }

    // ���� UI �ݱ�
    private void CloseSetting(ClickEvent evt)
    {
        SoundManager.Instance.PlaySound(Sound.ButtonClick);
        _settingUI.style.display = DisplayStyle.None;
        _startUI.style.display = DisplayStyle.Flex;
        
    }

    // ���� ����
    private void Close(ClickEvent evt)
    {
        Application.Quit();
    }
}
