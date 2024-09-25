using UnityEngine;
using UnityEngine.UIElements;

public class SettingsUI : MonoBehaviour
{
    private VisualElement root;
    private Slider effectSlider;
    private Slider bgMusicSlider;
    private Image soundIcon;
    private Image bgMusicIcon;

    private const string soundEffectIconPath = "Assets/Images/sound_effect_icon.png";
    private const string soundEffectMuteIconPath = "Assets/Images/sound_effect_mute_icon.png";
    private const string bgMusicIconPath = "Assets/Images/background_music_icon.png";
    private const string bgMusicMuteIconPath = "Assets/Images/background_music_mute_icon.png";

    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        // 슬라이더 및 이미지 요소 가져오기
        effectSlider = root.Q<Slider>("effectSlider");
        bgMusicSlider = root.Q<Slider>("bgMusicSlider");
        soundIcon = root.Q<Image>("soundIcon");
        bgMusicIcon = root.Q<Image>("bgMusicIcon");

        // 슬라이더 이벤트 등록
        effectSlider.RegisterValueChangedCallback(evt => UpdateSoundIcon(evt.newValue));
        bgMusicSlider.RegisterValueChangedCallback(evt => UpdateBgMusicIcon(evt.newValue));

        // 초기 아이콘 상태 설정
        UpdateSoundIcon(effectSlider.value);
        UpdateBgMusicIcon(bgMusicSlider.value);
    }

    private void UpdateSoundIcon(float value)
    {
        if (value == 0)
        {
            soundIcon.image = LoadImageFromPath(soundEffectMuteIconPath); // 음소거 이미지
        }
        else
        {
            soundIcon.image = LoadImageFromPath(soundEffectIconPath); // 일반 효과음 이미지
        }
    }

    private void UpdateBgMusicIcon(float value)
    {
        if (value == 0)
        {
            bgMusicIcon.image = LoadImageFromPath(bgMusicMuteIconPath); // 음소거 이미지
        }
        else
        {
            bgMusicIcon.image = LoadImageFromPath(bgMusicIconPath); // 일반 배경음 이미지
        }
    }

    private Texture2D LoadImageFromPath(string path)
    {
        return (Texture2D)Resources.Load(path);
    }
}
