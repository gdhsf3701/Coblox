using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using BackEnd.Tcp;

public class SendMessage : MonoBehaviour
{
    public static SendMessage Instance;
    
    private string UID;
    private static ChatManager _chatManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        UID = Backend.UID;
        _chatManager = GameObject.Find("ChatManager").GetComponent<ChatManager>();
    }

    public void SentMessage(string firstmessage,string lastmessage = "")
    {
        _chatManager.SendChat(firstmessage, lastmessage);
    }
    

}
