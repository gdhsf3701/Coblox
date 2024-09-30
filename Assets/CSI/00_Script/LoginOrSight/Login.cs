using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 뒤끝 SDK namespace 추가
using BackEnd;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField]private GameObject TeamSellct;
    [SerializeField]private TextMeshProUGUI Text,bt;
    [SerializeField]public TMP_InputField ID, PW, nicknamel;
    [SerializeField]private Image loginimage;
    [SerializeField]private Sprite Don,startIm;
    [SerializeField] public Button Back_Bt;

    private void Awake()
    {
        TeamSellct.SetActive(false);
        DataBaseScript.Instance.Resetvalue();
        var bro = Backend.Initialize(); // 뒤끝 초기화

        // 뒤끝 초기화에 대한 응답값
        if(bro.IsSuccess()) {
            Debug.Log("초기화 성공 : " + bro); // 성공일 경우 statusCode 204 Success
        } else {
            Debug.LogError("초기화 실패 : " + bro); // 실패일 경우 statusCode 400대 에러 발생
            SceneManager.LoadScene("01_Start");
        }
    }


    public void Login_bt()
    {

        if (nicknamel.text.Length > 6)//글자수 제한 6글자 이상 불가
        {
            
            Text.color = new Color(0.6603774f,0.1775543f,0.1775543f);
            Text.text = $"별명의 제한수는 6글자입니다. 당신의 글자수는 : {nicknamel.text.Length} 입니다.";
            return;

        }
        else if (!PW.interactable)//패스워드가 잠기지 않았으면
        {
            if (Backend.UserNickName != nicknamel.text)//자신의 이름과 다른사람의 이름이 같지 않으면
            {
                BackendReturnObject bro2 = Backend.BMember.CheckNicknameDuplication(nicknamel.text);
                if(!bro2.IsSuccess())
                {
                    Text.color = new Color(0.6603774f,0.1775543f,0.1775543f);

                    Text.text = "해당 닉네임으로 수정 불가능합니다.";
                    return;
                }
            }
            if (nicknamel.text.Length == 0)
            {
                Text.color = new Color(0.6603774f,0.1775543f,0.1775543f);
                Text.text = $"별명이 없습니다.";
                return;
            }

            //////////////////////////////////////////////////////////////게임 신 들어가기
            Text.color = Color.white;
            Text.text = "로그인중...";
        
            Backend.BMember.CreateNickname(nicknamel.text);
            /*var gild = Backend.Guild.GetMyGuildInfoV3();
            if (gild.IsSuccess())
            {
                DataBaseScript.Instance.inGuild = true;
            }
            else
            {
                print("길드 불러오기 실패");
            }*/
            Back_Bt.interactable = false;
            TeamSellct.SetActive(true);
            DataBaseScript.Instance.NicName = nicknamel.text;
            DataBaseScript.Instance.UID = Backend.UID;
            //SceneManager.LoadScene("03_Main");
            return;
            //////////////////////////////////////////////////게임신 들어가기 끝
        }
        ///////////////////////////////////////////////////////////로그인 하라

        nicknamel.interactable = false;

        Debug.Log("로그인을 요청합니다.");
        Text.color = Color.white;
        Text.text = "로그인 정보 확인중...";
        var bro = Backend.BMember.CustomLogin(ID.text, PW.text);
        
        
        if(bro.IsSuccess()&&PW.interactable)
        {
            Text.color = Color.green;
            Text.text = "로그인에 성공했습니다.";
            loginimage.sprite = Don;
            ID.interactable = false;
            PW.interactable = false;
            nicknamel.interactable = true;

            bt.fontSize = 13;
            bt.text = "별명 저장하기/시작하기";

            if (nicknamel.text == "")//닉네임 칸에아무것도 없으면?
            {
                print("닉네임 널");
                nicknamel.text = Backend.UserNickName;
                
            }
            else
            {

            }

        }
        else
        {
            Text.color = new Color(0.6603774f,0.1775543f,0.1775543f);
            Text.text = bro.GetMessage();

            /*switch (bro.GetStatusCode())
            {
                case "400":
                    Text.text = "필수 칸을 다시 한번 확인해주세요.";
                    break;
                case "401":
                    Text.text = "아이디 또는 비밀번호가 일치하지 않습니다";
                    break;
                case "403":
                    Text.text = "계정이 차단되었습니다. 개발자에게 문의주세요.";
                    break;
                default:
                    Text.text = "알 수 없는 오류";
                    break;
            }*/
        }
    }

    public void resetUI()
    {
        nicknamel.interactable = false;
        ID.interactable = true;
        PW.interactable = true;
        bt.fontSize = 21.5f;
        bt.text = "로그인";
        loginimage.sprite = startIm;
        ID.text = "";
        PW.text = "";
        nicknamel.text = "";
        Text.text = "";
    }
}
