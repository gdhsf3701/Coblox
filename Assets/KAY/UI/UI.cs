using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    private VisualElement _startUI;    // 시작 화면 UI
    private VisualElement _settingUI;  // 설정 UI

    private Button _Start;      // 게임 시작 버튼
    private Button Setting;     // 설정 버튼
    private Button close;       // 게임 종료 버튼
    private Button closeSetting; // 설정 닫기 버튼

    private Slider bgmSlider;   // BGM 볼륨 슬라이더
    private Slider soundSlider; // 사운드 볼륨 슬라이더

    private void Start()
    {
        // UI 문서로부터 VisualElement 가져오기
        var root = GetComponent<UIDocument>().rootVisualElement;

        // 버튼 요소들 가져오기
        _Start = root.Q<Button>("Start");
        Setting = root.Q<Button>("Setting");
        close = root.Q<Button>("Close");
        closeSetting = root.Q<Button>("CloseSetting");

        // 슬라이더 요소들 가져오기
        bgmSlider = root.Q<Slider>("BGMSlider");
        soundSlider = root.Q<Slider>("SoundSlider");

        // 화면 UI 요소들 가져오기
        _startUI = root.Q<VisualElement>("StartScene");
        _settingUI = root.Q<VisualElement>("SettingChang");

        // 이벤트 리스너 등록
        _Start.RegisterCallback<ClickEvent>(GameStart);
        Setting.RegisterCallback<ClickEvent>(OpenSetting);
        close.RegisterCallback<ClickEvent>(Close);
        closeSetting.RegisterCallback<ClickEvent>(CloseSetting);

        // 슬라이더 값 변경 시 볼륨 설정
        bgmSlider.RegisterValueChangedCallback(evt =>
        {
            SoundManager.Instance.SetBgmVolume(evt.newValue / 100f); // BGM 볼륨 변경
        });

        soundSlider.RegisterValueChangedCallback(evt =>
        {
            SoundManager.Instance.SetVolume(evt.newValue / 100f); // 효과음 볼륨 변경
        });

        // 기본 상태 설정: 시작 화면을 보여주고, 설정 화면 숨김
        _settingUI.style.display = DisplayStyle.None;
        _startUI.style.display = DisplayStyle.Flex;
    }

    // 게임 시작 시 호출
    private void GameStart(ClickEvent evt)
    {
        SoundManager.Instance.PlaySound(Sound.ButtonClick);
        SceneManager.LoadScene("02_LoginorSighUP");
    }

    // 설정 UI 열기
    private void OpenSetting(ClickEvent evt)
    {
        SoundManager.Instance.PlaySound(Sound.ButtonClick);
        _settingUI.style.display = DisplayStyle.Flex;
        _startUI.style.display = DisplayStyle.None;
       
    }

    // 설정 UI 닫기
    private void CloseSetting(ClickEvent evt)
    {
        SoundManager.Instance.PlaySound(Sound.ButtonClick);
        _settingUI.style.display = DisplayStyle.None;
        _startUI.style.display = DisplayStyle.Flex;
        
    }

    // 게임 종료
    private void Close(ClickEvent evt)
    {
        Application.Quit();
    }
}
