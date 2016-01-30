using UnityEngine;
using UnityEngine.Networking;
using System;

public class MsgTypes
{
    public static short MSG_CHAT = MsgType.Highest + 1;
    public static short MSG_SCORE = MsgType.Highest + 2;
}

public class ChatMessage : MessageBase
{
    public string text;
}

public class ScoreMessage: MessageBase
{
    public int score;
}


public class HighNetworkController : NetworkManager
{
    public EventManager eventManager;
    private NetworkClient myClient;
    private bool isClient;

    void Start()
    {
        eventManager.AddListener(EVENT_TYPE.SENDCHAT, SendChatMsgs);
        eventManager.AddListener(EVENT_TYPE.SENDSCORE, SendScoreMsgs);
    }

    public void SendChatMsgs(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        ChatMessage msgA = new ChatMessage();
        msgA.text = Param.ToString();
        if (isClient && myClient != null)
        {
            myClient.Send(MsgTypes.MSG_CHAT, msgA);
        }
        else
        {
            NetworkServer.SendToAll(MsgTypes.MSG_CHAT, msgA);
        }
        
        //Debug.Log("Sent " + msgA.text);
    }

    public void SendScoreMsgs(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        ScoreMessage msgA = new ScoreMessage();
        msgA.score = Int32.Parse(Param.ToString());
        if (isClient && myClient != null)
        {
            myClient.Send(MsgTypes.MSG_SCORE, msgA);
        }
        else
        {
            NetworkServer.SendToAll(MsgTypes.MSG_SCORE, msgA);
        }
    }

    public void StartClient(string ip)
    {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgTypes.MSG_CHAT, OnChatMessageReceived);
        myClient.RegisterHandler(MsgTypes.MSG_SCORE, OnScoreMessageReceived);
        myClient.Connect(ip, 7777);
        isClient = true;
    }

    void OnChatMessageReceived(NetworkMessage msg)
    {
        ChatMessage msgA = msg.ReadMessage<ChatMessage>();
        Debug.Log("Received Chat");
        eventManager.PostNotification(EVENT_TYPE.RECEIVECHAT, this, msgA.text.ToString());
    }

    void OnScoreMessageReceived(NetworkMessage msg)
    {
        ScoreMessage msgA = msg.ReadMessage<ScoreMessage>();
        Debug.Log("Received score" + msgA.score);
        eventManager.PostNotification(EVENT_TYPE.RECEIVESCORE, this, msgA.score.ToString());
    }  

    public void BeginHosting()
    {
        NetworkServer.Listen(7777);       
        NetworkServer.RegisterHandler(MsgTypes.MSG_CHAT, OnChatMessageReceived);
        NetworkServer.RegisterHandler(MsgTypes.MSG_SCORE, OnScoreMessageReceived);
    }
}

