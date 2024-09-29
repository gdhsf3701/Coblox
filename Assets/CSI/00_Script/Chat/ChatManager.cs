using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using BackndChat;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class PlayerInfo
{
    public string UID;
    public string Name;
    public int Rank;
    public int Point;

    public PlayerInfo(string uid, string name,int rank =0,int point = 0)
    {
        this.UID = uid;
        this.Name = name;
        this.Rank = rank;
        this.Point = point;
    }
}
public class ChatManager : MonoBehaviour ,BackndChat.IChatClientListener
{
    public static ChatManager Instance;
    private BackndChat.ChatClient ChatClient = null;
    public GameObject PlayerWaitLoding_GameObj,PlayerListUI;
    public Image GameStartImage;
    public string RoomName;
    public ulong RoomCode;
    public TextMeshProUGUI Info;
    public TMP_InputField RoomCodeInput;
    public bool Playing_Game;
    public bool isRoomOwner;
    private float LimitTime, NowTime;
    public string Message;
    public event Action<string> getMessage;
    public event Action PlzresetList;
    public List<PlayerInfo> players = new List<PlayerInfo>();

    public GameObject LoadingUI;
    string list;

    //[UD,이름,]

    public void resetthisScript()
    {
        RoomName = "";
        RoomCode = 0;
        Playing_Game = false;
        isRoomOwner = false;
        LimitTime = 0;
        NowTime = 0;
        players = new List<PlayerInfo>();
        GameStartImage.color = new Color(0.7924528f, 0, 0, 1);
    }
    
    public async Task Wait(int delay)
    {
        await Task.Delay(delay);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        LoadingUI.SetActive(true);
        PlayerListUI.SetActive(false);

        PlayerWaitLoding_GameObj.SetActive(false);

        Playing_Game = false;
        isRoomOwner = false;
        DataBaseScript.Instance.isOwner = false;

        DontDestroyOnLoad(this.gameObject);
        GameStartImage.color = new Color(0, 0.7921569f, 0.07352757f, 1);
        GameStartImage.color = new Color(0.6792453f, 0.6347173f, 0.1762193f, 1);
        GameStartImage.color = new Color(0.7924528f, 0, 0, 1);
        GameStartImage.GetComponent<Button>().interactable = false;

        

    }
    public void OnCreateRoom()
    {
        if (DataBaseScript.Instance.teacher)
        {
            DataBaseScript.Instance.isOwner = false;
            resetthisScript();
            isRoomOwner = true;
            DataBaseScript.Instance.isOwner = true;

            CreateprivateRoom(DateTime.Now.ToString());
            GameStartImage.GetComponent<Button>().interactable = false;

            GameStartImage.color = new Color(0.6792453f, 0.6347173f, 0.1762193f, 1);


        }
        else
        {
            print("Room을 생성할 권환이 없습니다.");
            GameStartImage.GetComponent<Button>().interactable = false;

            GameStartImage.color = new Color(0.7924528f, 0, 0, 1);

        }

    }

    public void OnjoinRoom()
    {
        print("게임 룸 입장 시도");
        JoinprivateRoom(ulong.Parse(RoomCodeInput.text));
    }
    
    void Update()
    {
        ChatClient?.Update();
        if (Input.GetKeyDown(KeyCode.K))
        {
            print(RoomName);
            print(RoomCode);
        }
    }


    public void GameStart_BT()//*TImer 는 초 기준*//
    {
        Send_GameStart(500);
    }

    public async void Send_GameStart(int Timer = 90)
    {
        if (isRoomOwner)
        {
            GameStartImage.GetComponent<Button>().interactable = false;

            GameStartImage.color = new Color(0.6792453f, 0.6347173f, 0.1762193f, 1);


            SendChat("Teacher",Backend.UserNickName);
            await Wait(1200);
            print(DataBaseScript.Instance.siteData.GetLength(0));
            for (int i = 0; i < DataBaseScript.Instance.siteData.GetLength(0); i++)
            {
                print(i);
                list += $"{DataBaseScript.Instance.siteData[i, 0]}$" +
                        $"{DataBaseScript.Instance.siteData[i, 1]}$" +
                        $"{DataBaseScript.Instance.siteData[i, 2]}$" +
                        $"{DataBaseScript.Instance.siteData[i, 3]}$" +
                        $"{DataBaseScript.Instance.siteData[i, 4]}$" +
                        $"{DataBaseScript.Instance.siteData[i, 5]}$" +
                        $"{DataBaseScript.Instance.siteData[i, 6]}$" +
                        $"{DataBaseScript.Instance.siteData[i, 7]}$" +
                        $"{DataBaseScript.Instance.siteData[i, 8]}`";

            }

            global::SendMessage.Instance.SentMessage("Munjea", list);
            await Wait(1200);
            SendChat("StartGame",Timer.ToString());//초 기준
        }
        else
        {
            GameStartImage.GetComponent<Button>().interactable = false;

            GameStartImage.color = new Color(0.7924528f, 0, 0, 1);

            print("권환이 없습니다.");
        }
    }



    
    
    private void Start()
    {
        ChatClient = new ChatClient(this, new ChatClientArguments//Exception: Nickname is Required in Backend ||| 예외: 백엔드에는 별명이 필요합니다.
        {
            Avatar = DataBaseScript.Instance.teacher?"Teacher":"Player"
            
        });
        
        

    }

    public void SendChat(string firstmessage,string lastmessage = "")
    {
        // 채팅 메시지를 입력 하는 함수 입니다. 채널 그룹, 채널 이름, 채널 번호, 메시지를 넣어서 보내 줍니다.
        ChatClient.SendChatMessage("main", RoomName, RoomCode,  $"{DataBaseScript.Instance.UID}:{firstmessage}:{lastmessage}");
    }
    public void CreateprivateRoom(string channelName)
    {
        // 프라이빗 채널을 생성 하는 함수 입니다. 채널 그룹, 채널 번호, 채널 이름, 최대 인원, 비밀번호을 넣어서 보내 줍니다.
        // 비밀번호를 넣지 않을 경우 공개 채널로 생성 됩니다.
        // 채널 번호를 0으로 보낼 경우 서버에서 자동으로 채널 번호를 부여 합니다.\
        
        ChatClient.SendCreatePrivateChannel("main", 0, channelName);

    }

    public void JoinprivateRoom(ulong channelNumber)
    {
        // 프라이빗 채널 타입의 채널 입장 함수 입니다. 채널 그룹, 채널 번호를 넣어서 보내 줍니다.
        ChatClient.SendJoinPrivateChannel("main",channelNumber, "");
        ChatClient.SendJoinPrivateChannel("main",channelNumber, "");
    }


    public void OnJoinChannel(ChannelInfo channelInfo)
    {
        LoadingUI.SetActive(false);
        print("채팅 채널 조인");
        print(channelInfo.ChannelName);
        print(channelInfo.ChannelNumber);
        print(RoomCode);
        if (RoomCode != channelInfo.ChannelNumber && RoomName != "server-1")
        {
            ChatClient.SendLeaveChannel("main",RoomName,RoomCode);
            print("이전 게임 룸 나감");
        }
        LoadingUI.SetActive(false);
        RoomCode = channelInfo.ChannelNumber;
        RoomName = channelInfo.ChannelName;
        if (DataBaseScript.Instance.teacher )
        {
            if(RoomCode == 1)
            {
                Info.text = $"RoomCode : Null";


            }
            else
            {
                Info.text = $"RoomCode : {RoomCode}";
                PlayerListUI.SetActive(true);
                StartCoroutine("WaitGameStart");

            }
        }
        if (RoomName != "server-1")
        {
            SendChat("PlayerJoin",Backend.UserNickName);

        }
    }

    IEnumerator WaitGameStart()
    {
        yield return new WaitForSecondsRealtime(4f);
        GameStartImage.GetComponent<Button>().interactable = true;
        GameStartImage.color = new Color(0, 0.7921569f, 0.07352757f, 1);
    }


    public void OnLeaveChannel(ChannelInfo channelInfo)
    {
         print("채팅 체널 나감");
         print(channelInfo.ChannelNumber);
         print(channelInfo.ChannelName);
         LoadingUI.SetActive(true);
         resetthisScript();
    }

    public void OnJoinChannelPlayer(string channelGroup, string channelName, UInt64 channelNumber, string gamerName,string avatar)
    {
        print($"player{gamerName}이/가 들어옴");//서버
        
        
    }

    public void OnLeaveChannelPlayer(string channelGroup, string channelName, UInt64 channelNumber, string gamerName,//서버
        string avatar)
    {
        foreach (var plr in players)
        {
            if (plr.Name == gamerName)
            {
                players.Remove(plr);
            }
        }
        PrintALLPlr();
        print($"{gamerName}이/가 나감");
    }


    public void PrintALLPlr()
    {
        Debug.Log("All players:");
        
        players = players.OrderByDescending(p => p.Point).ToList();
        foreach (var player in players)
        {
            Debug.Log("UID: " + player.UID + ", Name: " + player.Name);
        }      
        PlzresetList?.Invoke();
    }
    public void OnChatMessage(MessageInfo messageInfo)
    {
        print("챗이 옴");
        print(messageInfo.Message);
        if (messageInfo.ChannelNumber == RoomCode)
        {
            //Info.text = messageInfo.Message;
            Message = messageInfo.Message;
            print(Message.Split(":")[0]);
            print(Message.Split(":")[1]);
            //if(Message.Split(":")[0] == Backend.UID) return; // 자신의 정보는 차단
            string uid = Message.Split(":")[0];
            string message = Message.Split(":")[2];
            switch (Message.Split(":")[1])
            {
                case "PlayerJoin":
                    //자신이 왔다는 것을 알림
                    //클라이언트UID:PlayerJoin:플래이어 이름
                    print("플래이어가 들어옴" + message);
                    
                     // Ignoring the second element
                    if (uid == DataBaseScript.Instance.UID)
                    {
                        if (isRoomOwner)
                        {


                        }
                        else
                        {
                            PlayerWaitLoding_GameObj.SetActive(true);

                        }

                            
                    }
                    /*if(!isRoomOwner&& !DataBaseScript.Instance.teacher)
                    {
                        

                    }*/
                    Debug.Log("UID: " + uid + ", Name: " + message);
                    if (!Playing_Game)
                    {
                        PlayerInfo result = players.Find(player => player.Name == message && player.UID ==uid);
                        if (result == null)
                        {
                            players.Add(new PlayerInfo(uid, message));

                        }

                        PrintALLPlr();
                    }
                    else
                    {
                        foreach (var plr in players)
                        {
                            if (plr.UID == uid)
                            {
                                PrintALLPlr();
                                print("게임 시작중이지만 데이터가 있어서 승인");
                                return;
                            }
                        }
                        print("게임 시작중 입력 불가");
                    }
                    
                    break;
                
                case "Teacher":
                    //자신이 선셍닝이고 자신의 UIO를 보내는 코드
                    //서버UID:Teacher:선생님 계정 닉네임
                    print($"선생님 UID : {uid} || 선생님 별명 : {message}");
                    break;
                case "StartGame":
                    //게임 시작이 되었다는 것을 알림|| 서버 전용
                    //서버UID:StartGame:시간
                    Playing_Game = true;
                    print($"선생님 UID : {uid} || 제한 시간 : {message}");
                    LimitTime = float.Parse(message);
                    NowTime = float.Parse(message);
                    DataBaseScript.Instance.Time = float.Parse(message);
                    Playing_Game = true;
                    if(DataBaseScript.Instance.teacher && isRoomOwner){
                        SceneManager.LoadScene("04_1_TeathcerInGame");

                    }
                    else
                    {
                        SceneManager.LoadScene("RestaurantStart");

                    }


                    break;
                case "EndGame":
                    //게임 종료를 알리는 코드 || 서버에서 보내고 클라에서 받는다.
                    //서버UID:EndGame:
                    print($"선생님 UID : {uid} || 순위 : {message}");
                    /*    순위
                     * 1:uid:point/
                     * 2:uid:point/
                     * 3:uid:point/
                     * 4:uid:point/
                    */
                    
                    DataBaseScript.Instance.List = new List<(int Rank, string Name, int Point)>();

                    string[] rankMessages = message.Split('/');
                    for (int i = 0; i < rankMessages.Length; i++)
                    {
                        string[] parts = rankMessages[i].Split('@');
                        if (parts.Length == 3)
                        {
                            int rank = int.Parse(parts[0]);
                            string ui0d = parts[1];
                            int point = int.Parse(parts[2]);
                            DataBaseScript.Instance.List.Add((rank, ui0d, point));

                        }
                    }

                    
                    SceneManager.LoadScene("05_EndGame");

                    Playing_Game = false;
                    isRoomOwner = false;
                    //DataBaseScript.Instance.isOwner = false;
                    
                    foreach (var rank in DataBaseScript.Instance.List)
                    {
                        Debug.Log($"Rank: {rank.Rank}, UID: {rank.Name}, Point: {rank.Point}");

                    }
                    Destroy(gameObject);




                    break;
                case "Leve":
                    // 채널을 떠나는 것을 처리하는 코드
                    //클라이언트UID:Leve:
                    print($"클라이언트 UID : {uid}");

                    

                    break;
                case "GetPoint":
                    //포인트를 얻었을때 보내는 코드 || 서버에서 받고 클라에서 보낸다
                    //클라이언트UID:GetPoint:총점수
                    
                    print($"클라이언트 UID : {uid} || 총 점수 : {message}");

                    foreach (var plr in players)
                    {
                        if (plr.UID == uid)
                        {
                            plr.Point = int.Parse(message);
                        }
                    }

                    PrintALLPlr();
                    getMessage?.Invoke(messageInfo.Message);
                    break;
                case "Munjea":
                    // 문제를 수동으로 주는 Test Case
                    //클라이언트UID:Munjea:차트 번호
                    if (!isRoomOwner)//클라
                    {
                        GameObject obj = Instantiate(DataBaseScript.Instance.group, GameObject.Find("Canvas").transform);
                        var msg = Message.Split(":")[2];
                        int a = 0;
                        DataBaseScript.Instance.siteData = new string[10,9]; 
                        foreach (var msgs in msg.Split("`"))
                        {
                            //문제1$(2-3i)(5+4i)$1$11$21$31$41$1$1
                            print(msgs);
                            print(a);
                            DataBaseScript.Instance.siteData[a, 0] = msgs.Split("$")[0];
                            DataBaseScript.Instance.siteData[a,1] = msgs.Split("$")[1]; 
                            DataBaseScript.Instance.siteData[a,2] = msgs.Split("$")[2]; 
                            DataBaseScript.Instance.siteData[a,3] = msgs.Split("$")[3]; 
                            DataBaseScript.Instance.siteData[a,4] = msgs.Split("$")[4]; 
                            DataBaseScript.Instance.siteData[a,5] = msgs.Split("$")[5]; //여기 분배 다시 해야함..... ':' -> '$'
                            DataBaseScript.Instance.siteData[a,6] = msgs.Split("$")[6];
                            DataBaseScript.Instance.siteData[a,7] = msgs.Split("$")[7];
                            DataBaseScript.Instance.siteData[a,8] = msgs.Split("$")[8];
                            a++;
                            if(DataBaseScript.Instance.siteData.GetLength(0) < a)
                            {
                                return;
                            }
                        }
                        //////////////////////////////////////////////////////////////////////////////////////
                        /*obj.GetComponent<Makemunja>().Getnayung(
                            msg.Split("$")[0], 
                            msg.Split("$")[1], 
                            msg.Split("$")[2], 
                            msg.Split("$")[3], 
                            msg.Split("$")[4], 
                            msg.Split("$")[5], //여기 분배 다시 해야함..... ':' -> '$'
                            msg.Split("$")[6],
                            msg.Split("$")[7],
                            msg.Split("$")[8]);*/
                        

                        
                            

                    }


                    break;
                case "Test":
                    //테스트 Case
                    print("");
                    break;
                case "":
                    print("이건 뭐냐");
                    break;
                default:
                    print("No matched case for " + Message.Split(":")[1]);
                    break;
                    
            }

        }
    }
    
    public void OnWhisperMessage(WhisperMessageInfo messageInfo) { }

    public void OnTranslateMessage(List<MessageInfo> messages) { }

    public void OnHideMessage(MessageInfo messageInfo) { }

    public void OnDeleteMessage(MessageInfo messageInfo) { }

    public void OnSuccess(SUCCESS_MESSAGE success, object param)
    {
        print("성공");
        print(success.ToString());
        switch(success)
        {
            default:
                break;
        }
    }

    public void OnError(ERROR_MESSAGE error, object param)
    {
        print("에러");
        print(error.ToString());

        if (GameStartImage)
        {
            GameStartImage.GetComponent<Button>().interactable = false;

            GameStartImage.color = new Color(0.7924528f, 0, 0, 1);
        }

        switch(error)
        { 
            case(ERROR_MESSAGE.ALREADY_JOIN_CHANNEL):
                break;
            case(ERROR_MESSAGE.NOT_JOIN_CHANNEL):
                resetthisScript();
                PrintALLPlr();
                PlayerListUI.SetActive(false);
                GameStartImage.GetComponent<Button>().interactable = false;
                GameStartImage.color = new Color(0.7924528f, 0, 0, 1);

                break;
            default:
                break;
        }
    }
    
    
    public void Edit_DataBase_Point(int Final_point)
    {
        PlayerInfo result = players.Find(player => player.UID ==DataBaseScript.Instance.UID && player.Name == DataBaseScript.Instance.NicName);
        if (result != null)
        {
            result.Point = Final_point;

        }
        global::SendMessage.Instance.SentMessage("GetPoint",$"{Final_point}");
        PrintALLPlr();
    }
    


    /*private void OnApplicationQuit()
    {
        ChatClient.SendLeaveChannel("Main",RoomName,RoomCode);
        ChatClient?.Dispose();
    }*/
}
