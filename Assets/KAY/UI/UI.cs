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

        // �����̴� �� �̹��� ��� ��������
        effectSlider = root.Q<Slider>("effectSlider");
        bgMusicSlider = root.Q<Slider>("bgMusicSlider");
        soundIcon = root.Q<Image>("soundIcon");
        bgMusicIcon = root.Q<Image>("bgMusicIcon");

        // �����̴� �̺�Ʈ ���
        effectSlider.RegisterValueChangedCallback(evt => UpdateSoundIcon(evt.newValue));
        bgMusicSlider.RegisterValueChangedCallback(evt => UpdateBgMusicIcon(evt.newValue));

        // �ʱ� ������ ���� ����
        UpdateSoundIcon(effectSlider.value);
        UpdateBgMusicIcon(bgMusicSlider.value);
    }

    private void UpdateSoundIcon(float value)
    {
        if (value == 0)
        {
            soundIcon.image = LoadImageFromPath(soundEffectMuteIconPath); // ���Ұ� �̹���
        }
        else
        {
            soundIcon.image = LoadImageFromPath(soundEffectIconPath); // �Ϲ� ȿ���� �̹���
        }
    }

    private void UpdateBgMusicIcon(float value)
    {
        if (value == 0)
        {
            bgMusicIcon.image = LoadImageFromPath(bgMusicMuteIconPath); // ���Ұ� �̹���
        }
        else
        {
            bgMusicIcon.image = LoadImageFromPath(bgMusicIconPath); // �Ϲ� ����� �̹���
        }
    }

    private Texture2D LoadImageFromPath(string path)
    {
        return (Texture2D)Resources.Load(path);
    }
}
