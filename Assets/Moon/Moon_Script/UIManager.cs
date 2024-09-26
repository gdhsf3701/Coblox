using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening.Core.Easing;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] StartSonNom sonNom;
    [SerializeField]private string[] sonNomTalkText;
    [SerializeField] float doneTime;
    [SerializeField]GameObject buttonYes, buttonNo;
    string nowText;
    private void Awake()
    {
        text.text = "";
        sonNom.OnDone += NowChengeText;
        buttonYes.SetActive(false);
        buttonNo.SetActive(false);
        text.gameObject.SetActive(false);
    }
    private void NowChengeText()
    {
        nowText = sonNomTalkText[Random.Range(0,sonNomTalkText.Length)];
        StartCoroutine(TextGo());
    }
    private IEnumerator TextGo()
    {
        text.gameObject.SetActive(true);
        for (int i = 0; i < nowText.Length; i++) 
        { 
            text.text += nowText[i];
            yield return new WaitForSeconds(doneTime/nowText.Length);
        }
        buttonYes.SetActive(true);
        buttonNo.SetActive(true);
    }
    public void ClickButtonYes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ClickButtonNo()
    {
        buttonYes.SetActive(false);
        buttonNo.SetActive(false);
        text.text = "";
        text.gameObject.SetActive(false);
        StartCoroutine(sonNom.NextDoneChenge());
    }
}
