using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Option : MonoBehaviour
{
    [SerializeField] private VisualElement rootElement; // UI Document의 Root Element
    private Slider soundSlider; // 사운드 볼륨 슬라이더 (UI Toolkit)
    private Slider musicSlider; // 음악 볼륨 슬라이더 (UI Toolkit)
    private Label soundLabel; // 사운드 볼륨 텍스트
    private Label musicLabel; // 음악 볼륨 텍스트

    private bool isOptionPanelActive = false; // 옵션 패널 활성화 여부 플래그

    private void Start()
    {
        // UIDocument에서 Root VisualElement 가져오기
        var uiDocument = GetComponent<UIDocument>();
        rootElement = uiDocument.rootVisualElement;

        // 슬라이더와 레이블 가져오기
        soundSlider = rootElement.Q<Slider>("SoundSlider");
        musicSlider = rootElement.Q<Slider>("MusicSlider");
        soundLabel = rootElement.Q<Label>("SoundLabel");
        musicLabel = rootElement.Q<Label>("MusicLabel");

        // 슬라이더 초기값 설정
        soundSlider.value = SoundManager.Instance.GetVolume() * 10; // 0~1 값을 0~10으로 변환
        musicSlider.value = SoundManager.Instance.GetBgmVolume() * 10;

        // 슬라이더 값 변경 시 이벤트 등록
        soundSlider.RegisterValueChangedCallback(evt =>
        {
            SoundManager.Instance.SetVolume(evt.newValue / 10f); // 0~10 값을 0~1로 변환
            UpdateSoundText();
        });

        musicSlider.RegisterValueChangedCallback(evt =>
        {
            SoundManager.Instance.SetBgmVolume(evt.newValue / 10f);
            UpdateMusicText();
        });

        // 초기 텍스트 설정
        UpdateSoundText();
        UpdateMusicText();
    }

    // 옵션 패널을 토글하는 메서드
    public void ToggleOptionPanel()
    {
        isOptionPanelActive = !isOptionPanelActive;
        rootElement.Q<VisualElement>("OptionPanel").style.display = isOptionPanelActive ? DisplayStyle.Flex : DisplayStyle.None;
    }

    // 사운드 볼륨 텍스트 업데이트
    private void UpdateSoundText()
    {
        soundLabel.text = Mathf.RoundToInt(SoundManager.Instance.GetVolume() * 10).ToString(); // 0~10 범위 값으로 표시
    }

    // 음악 볼륨 텍스트 업데이트
    private void UpdateMusicText()
    {
        musicLabel.text = Mathf.RoundToInt(SoundManager.Instance.GetBgmVolume() * 10).ToString();
    }
}
