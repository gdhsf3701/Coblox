using UnityEngine;

// 뒤끝 SDK namespace 추가
using BackEnd;
using TMPro;


public class sighup : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI Text;
    [SerializeField] private TMP_InputField ID, PW, nickname;

    

    private void Start()
    {

        //Backend.BMember.CustomSignUp(ID.text, PW.text);

    }

    public void sighup_bt()
    {
        if (nickname.text.Length == 0)
        {
            Text.color = new Color(0.6603774f,0.1775543f,0.1775543f);
            Text.text = $"별명이 없습니다.";
            return;
        }
        if (nickname.text.Length > 6)//글자수 제한 6글자 이상 불가
        {
            Text.color = new Color(0.6603774f,0.1775543f,0.1775543f);
            Text.text = $"별명의 제한수는 6글자입니다. 당신의 글자수는 : {nickname.text.Length} 입니다.";
            return;

        }
        else
        {
            Text.text = $"";
            print($"ID : {ID.text} PW : {PW.text}");
            BackendReturnObject bro = Backend.BMember.CustomSignUp(ID.text,PW.text);
        
        
            if(bro.IsSuccess())
            {
                Text.color = Color.green;
                Backend.BMember.CreateNickname(nickname.text);

                Text.text = "회원가입에 성공했습니다.";
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
                    case "409":
                        Text.text = "이미 존재하는 아이디입니다.";
                        break;
                    default:
                        Text.text = "알 수 없는 오류";
                        break;
                }*/
            }
        }
        
    }
    public void resetUI()
    {
        ID.interactable = true;
        PW.interactable = true;
        ID.text = "";
        PW.text = "";
        Text.text = "";
    }    
}
