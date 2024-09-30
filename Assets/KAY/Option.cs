using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Option : MonoBehaviour
{
    [SerializeField] private VisualElement rootElement; // UI Document�� Root Element
    private Slider soundSlider; // ���� ���� �����̴� (UI Toolkit)
    private Slider musicSlider; // ���� ���� �����̴� (UI Toolkit)
    private Label soundLabel; // ���� ���� �ؽ�Ʈ
    private Label musicLabel; // ���� ���� �ؽ�Ʈ

    private bool isOptionPanelActive = false; // �ɼ� �г� Ȱ��ȭ ���� �÷���

    private void Start()
    {
        // UIDocument���� Root VisualElement ��������
        var uiDocument = GetComponent<UIDocument>();
        rootElement = uiDocument.rootVisualElement;

        // �����̴��� ���̺� ��������
        soundSlider = rootElement.Q<Slider>("SoundSlider");
        musicSlider = rootElement.Q<Slider>("MusicSlider");
        soundLabel = rootElement.Q<Label>("SoundLabel");
        musicLabel = rootElement.Q<Label>("MusicLabel");

        // �����̴� �ʱⰪ ����
        soundSlider.value = SoundManager.Instance.GetVolume() * 10; // 0~1 ���� 0~10���� ��ȯ
        musicSlider.value = SoundManager.Instance.GetBgmVolume() * 10;

        // �����̴� �� ���� �� �̺�Ʈ ���
        soundSlider.RegisterValueChangedCallback(evt =>
        {
            SoundManager.Instance.SetVolume(evt.newValue / 10f); // 0~10 ���� 0~1�� ��ȯ
            UpdateSoundText();
        });

        musicSlider.RegisterValueChangedCallback(evt =>
        {
            SoundManager.Instance.SetBgmVolume(evt.newValue / 10f);
            UpdateMusicText();
        });

        // �ʱ� �ؽ�Ʈ ����
        UpdateSoundText();
        UpdateMusicText();
    }

    // �ɼ� �г��� ����ϴ� �޼���
    public void ToggleOptionPanel()
    {
        isOptionPanelActive = !isOptionPanelActive;
        rootElement.Q<VisualElement>("OptionPanel").style.display = isOptionPanelActive ? DisplayStyle.Flex : DisplayStyle.None;
    }

    // ���� ���� �ؽ�Ʈ ������Ʈ
    private void UpdateSoundText()
    {
        soundLabel.text = Mathf.RoundToInt(SoundManager.Instance.GetVolume() * 10).ToString(); // 0~10 ���� ������ ǥ��
    }

    // ���� ���� �ؽ�Ʈ ������Ʈ
    private void UpdateMusicText()
    {
        musicLabel.text = Mathf.RoundToInt(SoundManager.Instance.GetBgmVolume() * 10).ToString();
    }
}
