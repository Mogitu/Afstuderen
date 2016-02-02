﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;


/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Highlevel networkmanager
/// TODO    :   Decouple message handling?
/// </summary>
public class NetworkController : MonoBehaviour
{
    public EventManager eventManager;
    public int socketPort=7777;

    private NetworkClient myClient;
    private bool isClient;


    //Experimental
    private List<NetworkClient> clients;

    void Start()
    {
        eventManager.AddListener(EVENT_TYPE.SENDCHAT, SendChatMsgs);
        eventManager.AddListener(EVENT_TYPE.SENDSCORENETWORK, SendScoreMsgs);
    }

    public void BeginHosting()
    {
        NetworkServer.Listen(socketPort);
        NetworkServer.RegisterHandler(MsgTypes.MSG_CHAT, OnChatMessageReceived);
        NetworkServer.RegisterHandler(MsgTypes.MSG_SCORE, OnScoreMessageReceived);
        NetworkServer.RegisterHandler(MsgType.Connect, OnPlayerConnect);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnPlayerDissConnect);

    }

    public void StartClient(string ip)
    {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgTypes.MSG_CHAT, OnChatMessageReceived);
        myClient.RegisterHandler(MsgTypes.MSG_SCORE, OnScoreMessageReceived);
        myClient.RegisterHandler(MsgType.Disconnect, OnPlayerDissConnect);
        myClient.Connect(ip, socketPort);
        isClient = true;
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
    }

    public void SendScoreMsgs(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {        
        ScoreMessage msgA = new ScoreMessage();
        msgA.score = (int)Param;
        Debug.Log("sending as score: "+ msgA.score);
        if (isClient && myClient != null)
        {
            myClient.Send(MsgTypes.MSG_SCORE, msgA);
        }
        else
        {
            NetworkServer.SendToAll(MsgTypes.MSG_SCORE, msgA);
        }
    }    

    void OnChatMessageReceived(NetworkMessage msg)
    {
        ChatMessage msgA = msg.ReadMessage<ChatMessage>();
        Debug.Log("Received Chat");
        eventManager.PostNotification(EVENT_TYPE.RECEIVECHATNETWORK, this, msgA.text.ToString());
    }

    void OnScoreMessageReceived(NetworkMessage msg)
    {
        ScoreMessage msgA = msg.ReadMessage<ScoreMessage>();
        Debug.Log("Received score " + msgA.score);
        eventManager.PostNotification(EVENT_TYPE.RECEIVESCORENETWORK, this, msgA.score);
    }

    void OnPlayerConnect(NetworkMessage msg)
    {  
        eventManager.PostNotification(EVENT_TYPE.PLAYERJOINED, this, "JOINED");
        if(NetworkServer.connections.Count >=2)
        {
            Debug.Log("Game is ready");
        }
    }

    void OnPlayerDissConnect(NetworkMessage msg)
    {
        eventManager.PostNotification(EVENT_TYPE.PLAYERLEFT, this, "LEFT");
    }   

    public int GetConnectionsCount
    {
        get { return NetworkServer.connections.Count; }
    }
}

//Contains message types thate are used in the game
public class MsgTypes
{
    public static short MSG_CHAT = MsgType.Highest + 1;
    public static short MSG_SCORE = MsgType.Highest + 2;
    public static short MSG_PLAYER_JOINED = MsgType.Highest + 3;
    public static short MSG_PLAYER_LEFT = MsgType.Highest + 4;
}

//All custom messages below.
public class ChatMessage : MessageBase
{
    public string text;
}

public class ScoreMessage : MessageBase
{
    public int score;
}

public class PlayerLeftMessage : MessageBase
{
    public string text;
}

public class PlayerJoinedMessage : MessageBase
{
    public string text;
}

