using System;
using System.Collections;
using System.Text;

using TexDrawLib;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;

using UnityEngine.UI;

public class SetImage : MonoBehaviour
{
    private TEXDraw _tex;
    private string url = "http://localhost:5000/api/runscript";

    private void Awake()
    {
        _tex = GetComponent<TEXDraw>();
    }
    
    private string changetext(string susick)
    {
        susick = susick.Replace("\u00a0", "");
        susick = susick.Replace("\\u00a0", "");
        //사칙 연산
        susick = susick.Replace("TIMES", "\\times");//X
        susick = susick.Replace("DIVIDE", "\\div");// /
        susick = susick.Replace("!=", "\\neq");// 같지 않다
        susick = susick.Replace(" +-", "\\pm");//플마
        susick = susick.Replace("+-", "\\pm");//플마
        susick = susick.Replace("-+", "\\mp");//마플
        


        //기호
        susick = susick.Replace("INF", "\\infty");//무한
        //susick = susick.Replace("alpha", "\\alpha");//알파
        //susick = susick.Replace("beta", "\\beta");//베타
        //susick = susick.Replace("DELTA", "\\Delta");//데타
        //susick = susick.Replace("delta", "\\delta");//데타
        //susick = susick.Replace("theta", "\\theta");//테타
        //susick = susick.Replace("gamma", "\\gamma");//테타
        susick = susick.Replace(".", "\\cdot ");//점
        susick = susick.Replace("prime", "^\\prime");//미분 기호 '다시'
        susick = susick.Replace("CIRC", "\\circ");//점
        susick = susick.Replace("REL rarrow", "\\xrightarrow");//위 아래 숫자가 있는     
        susick = susick.Replace("rarrow", "\\rightarrow");//화살표
        susick = susick.Replace("rarrow", "\\rightarrow");//화살표
        susick = susick.Replace("~ RARROW ~", "\\Rightarrow ");//화살표

        
        //집합
        susick = susick.Replace("SUBSET", "\\subset");
        susick = susick.Replace("SUPERSET", "\\supset");
        susick = susick.Replace("IN", "\\in");
        susick = susick.Replace("OWNS", "\\ni");
        susick = susick.Replace("EMPTYSET", "\\varnothing");//공집합
        

        susick = susick.Replace("`", "\\;");//띄어쓰기
        //susick = susick.Replace("sqrt", "\\sqrt");//루트
        susick = susick.Replace("LEFT ( pile{", "\\binom ");//파이방지
        susick = susick.Replace("} \\; RIGHT )", "");//파이방지
        susick = susick.Replace("#", " ");//파이방지
        //susick = susick.Replace("pi", "\\pi");//파이
        //susick = susick.Replace("sum", "\\sum");//파이
        susick = susick.Replace("LEFT ", "\\left\\");//이중 괄호
        susick = susick.Replace("RIGHT ", "\\right\\");//이중 괄호
        susick = susick.Replace("vert", "|");//절대값
        susick = susick.Replace("ATT", "\\circledast");//주의
        
        
        //부호
        
        susick = susick.Replace(">=", "\\geq");
        susick = susick.Replace("<=", "\\leq");
        
        //글씨 체
        
        susick = susick.Replace("bold", "\\mathbf");

        
        //최종 빈칸 삭제

        return susick;
    }

    private string Fracmanage(string susick)
    {
        int findIndex = susick.IndexOf("over");
        int Index = findIndex;
        if (Index != -1)
        {
            int agin = -1;
            do
            {
                Index--;
                if (susick[Index] == '}')
                {
                    if (agin == -1)
                    {
                        agin = 1;

                    }else
                        agin++;

                } else if (susick[Index] == '{')
                {
                    agin--;
                    if (agin == 0)
                    {
                        susick = susick.Remove(findIndex,5);//절대값
                        susick = susick.Insert(Index, "\\frac ");
                        break;
                    }
                }
            } while (findIndex != 0 || agin != 0);
            if (susick.Contains("over"))
            {
                susick = Fracmanage(susick);
            }
            return susick;
        }

        return null;
    }
    
    [System.Serializable]
    public class RequestData
    {
        public string param;
    }
    public IEnumerator CallPythonScript(string single_param,Action<string> callback)
    {
        
        RequestData requestData = new RequestData { param = single_param };
        string json = JsonUtility.ToJson(requestData);
        // json에서 'param' 키가 들어간 것을 확인하세요.
        print($"Sending JSON data: {json}");
        using UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer(); 
        request.SetRequestHeader("Content-Type", "application/json");
        
        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            print("Error: " + request.error);
        }
        else
        {
            print(request.downloadHandler.text.Replace("\\\\", "\\").Replace("\"",""));
            callback(request.downloadHandler.text.Replace("\\\\", "\\").Replace("\"",""));
        }
    }


    public void GetTexture(string susick)
    {
        StartCoroutine(CallPythonScript(susick, s =>
        {
            susick = s;
            print(susick);
            susick = changetext(susick);
            _tex.text = susick;
        }));
        /*susick = changetext(susick);
        if (susick.Contains("over"))
        {


            susick = Fracmanage(susick);
        }*/


        /*var url = $"https://latex.codecogs.com/png.image?\\huge&space;\\dpi{{500}}{susick}";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D img =((DownloadHandlerTexture)www.downloadHandler).texture;
            _image.transform.localScale = new Vector3(img.width,img.height);
            _image.texture = img;
        }*/
    }


}
