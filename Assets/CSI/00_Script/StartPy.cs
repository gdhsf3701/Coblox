
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
    public static StartPy Instance;

    private string url = "http://localhost:5000/api/CSIIMNIKASTART";
    
    private void Awake()
    {
        
        if (Instance == null)
        {
            print("자신을 Instance으로 선언");
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            var directoryPath = new DirectoryInfo(Application.dataPath + "/Resources/Start/");

            if (directoryPath.Exists)
            {
                string savedFilePath = "StartPy" + ".exe";
                string AllPath = directoryPath + savedFilePath;
                //Debug.Log(AllPath);
                if (File.Exists(AllPath))
                {
                    
                    Process.Start(Application.dataPath + "\\Resources\\Start\\StartPy.exe");
                }
            }
        }
        else
        {
            print("자신 지우기");
            Destroy(gameObject);
        }
        
        

    }

    private void OnApplicationQuit()
    {
        Disable_Py();
        //게임 중지때 StartPy 중지
    }

    void Start()
    {
        print("실행");
    }

    public void Disable_Py()
    {
        var processList = DI.Process.GetProcessesByName("StartPy");
        if(processList.Length > 0)
        {
            print("프로세스가 1개이상 동작중..");
            for (int i = 0; i < processList.Length; i++)
            {
                processList[i].Kill();
            }
            /*foreach(Process process in processList)
            {
                if (process.ProcessName.StartsWith("StartPy"))
                {
                    process.Kill();
                }
            }*/
        }
        else
        {
            print("실행된 프로세스 없음");
        }

        
    }


    [System.Serializable]
    public class RequestData
    {
        public string param;
        
    }

    

}
