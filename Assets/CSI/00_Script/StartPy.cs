using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System.Diagnostics;
using System.IO;
using DI = System.Diagnostics;


public class StartPy : MonoBehaviour
{
    
    private string url = "http://localhost:5000/api/CSIIMNIKASTART";
    public bool Startbool;
    private void Awake()
    {

        print(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
        print(Application.persistentDataPath);
        print(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
        //Environment.SpecialFolder.Desktop
        
        //Environment.SpecialFolder.Personal
        string path = Application.persistentDataPath;

        //Process.Start(path + "/Start/CSIIMNIKASTART.exe");


    }

    void Start()
    {
        string single_param = "9-3 DIVIDE  {1} over {3}+1=?";
        
        StartCoroutine(CallPythonScript(single_param));
        print("실행");
        var processList = DI.Process.GetProcessesByName("CSIIMNIKASTART");
        if(processList.Length > 0)
        {
            print("프로세스가 1개이상 동작중..");
            foreach(Process process in Process.GetProcesses())
            {
                print(process.ProcessName);
                if (process.ProcessName.StartsWith("CSIIMNIKASTART"))
                {
                    process.Kill();
                }
            }
        }
        else
        {
            print("실행된 프로세스 없음");
        }
    }
    
    public void StartPython()
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        processStartInfo.FileName = "C:\\Users\\jjsju\\AppData\\Local\\Programs\\Python\\Python312\\python.exe"; // 파이썬 실행 파일의 위치를 따옴표 안에 입력하세요.
        processStartInfo.Arguments = string.Format("{0}", "C:\\Users\\jjsju\\PycharmProjects\\hml-equation-parser\\Start.py"); // 파이썬 스크립트 파일의 전체 경로를 입력하세요.
        processStartInfo.UseShellExecute = false;
        processStartInfo.RedirectStandardOutput = true;
        using(Process process = Process.Start(processStartInfo))
        {
            using(StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                print(result);
            }
        }
    }

    private void Update()
    {
        if (Startbool)
        {
            string single_param = "9-3   {1} over {3}+1=?";

            StartCoroutine(CallPythonScript(single_param));
            Startbool = false;
        }
    }


    [System.Serializable]
    public class RequestData
    {
        public string param;
    }
    IEnumerator CallPythonScript(string single_param)
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
            print("Received: " + request.downloadHandler.text);
        }
    }
    
    

}
