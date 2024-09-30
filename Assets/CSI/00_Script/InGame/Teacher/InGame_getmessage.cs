using System;
using System.Collections;
using System.Collections.Generic;
using BackEnd;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InGame_getmessage : MonoBehaviour
{
    private ChatManager _chatManager;
    public GameObject LankingBar,lankingBode;
    
    private void Start()
    {
        _chatManager = GameObject.Find("ChatManager").GetComponent<ChatManager>();
        _chatManager.getMessage += Getmassage;
        _chatManager.PlzresetList += Makelanking;
        Makelanking();
    }

    private void OnDisable()
    {
        _chatManager.getMessage -= Getmassage;
        _chatManager.PlzresetList -= Makelanking;
    }

    
    
    public void SendEndData(){
        string mesg = "";
        int num = 0;
        foreach (var player in _chatManager.players)
        {
            mesg += $"{++num}@{player.Name}@{player.Point}/";
        }

        global::SendMessage.Instance.SentMessage("EndGame",mesg);
    }
    private void Makelanking()
    {
        print(lankingBode);
        print(LankingBar);
        foreach (Transform child in lankingBode.transform)
        {
            Destroy(child.gameObject);
        }
        
        if (_chatManager.players != null)
        {
            int num = 0;
            foreach(var player in _chatManager.players)
            {
                player.Rank = ++num;
                Debug.Log($"Player Ranking #{player.Rank} Name: {player.Name}");
                GameObject clone = Instantiate(LankingBar, lankingBode.transform);
                clone.GetComponent<RankingInfo>().No.text = player.Rank.ToString();
                clone.GetComponent<RankingInfo>().Name.text = player.Name.ToString();
                clone.GetComponent<RankingInfo>().Point.text = player.Point.ToString();
            }
        }
        else
        {
            Debug.Log("No players available for ranking.");
        }
    }

    private void Getmassage(string msg)
    {
        print(msg);
    }
}
