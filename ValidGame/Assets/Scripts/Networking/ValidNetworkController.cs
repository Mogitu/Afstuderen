﻿using UnityEngine;
using UnityEngine.Networking;
using AMC.Networking;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Highlevel networkmanager
/// </summary>

public class ValidNetworkController : NetworkController
{
    public EventManager EventManager;
    public int SocketPort = 7777;    

    void Start()
    {
        EventManager.AddListener(GameEvents.SendSchat, SendChatMsgs);
        EventManager.AddListener(GameEvents.SendScoreNetwork, SendScoreMsgs);
        EventManager.AddListener(GameEvents.SendCardToOpponent, SendOpponentCard);
    }

    public void SendOpponentCard(short Event_Type, Component Sender, object param = null)
    {       
        CardToOpponentMessage msgA = (CardToOpponentMessage)param;    
        NetworkContext.SendMessage(NetworkMessages.OpponentCard, msgA);
    }

    public void OnOpponentCardReceived(NetworkMessage msg)
    {
        CardToOpponentMessage msgA = msg.ReadMessage<CardToOpponentMessage>();     
        string name = msgA.CardName.Trim();
        GameObject go = GameObject.Find(name).gameObject;
        go.transform.position = msgA.Position;
        EventManager.PostNotification(GameEvents.CardReceivedFromOpponent,this, go);
    }

    public void StartHosting()
    {   
        CreateServerContext<AmcServer>(SocketPort);
        AddHandlers();  
        IsClient = false;
    }

    public  void StartClient(string ip)
    {    
        CreateClientContext<AmcClient>(ip,SocketPort);
        AddHandlers();
        IsClient = true;
    }

    public void SendChatMsgs(short event_Type, Component sender, object param = null)
    {
        ChatMessage msgA = new ChatMessage();
        msgA.Text = param.ToString();
        NetworkContext.SendMessage(NetworkMessages.MsgChat, msgA);      
    }

    public void SendScoreMsgs(short event_Type, Component sender, object param = null)
    {
        ScoreMessage msgA = new ScoreMessage();
        msgA.Score = (int)param;        
        NetworkContext.SendMessage(NetworkMessages.MsgScore, msgA);       
    }

    void OnChatMessageReceived(NetworkMessage msg)
    {
        ChatMessage msgA = msg.ReadMessage<ChatMessage>();       
        EventManager.PostNotification(GameEvents.ReceiveChatNetwork, this, msgA.Text.ToString());
    }

    void OnScoreMessageReceived(NetworkMessage msg)
    {
        ScoreMessage msgA = msg.ReadMessage<ScoreMessage>();       
        EventManager.PostNotification(GameEvents.ReceiveScoreNetwork, this, msgA.Score);
    }

    void OnPlayerConnect(NetworkMessage msg)
    {
        EventManager.PostNotification(GameEvents.PlayerJoined, this, "Client joined");      
    }

    void OnPlayerDissConnect(NetworkMessage msg)
    {
        EventManager.PostNotification(GameEvents.PlayerLeft, this, "LEFT");
    }

    public override void AddHandlers()
    {
        NetworkContext.RegisterHandler(NetworkMessages.MsgChat, OnChatMessageReceived);
        NetworkContext.RegisterHandler(NetworkMessages.MsgScore, OnScoreMessageReceived);
        NetworkContext.RegisterHandler(MsgType.Connect, OnPlayerConnect);
        NetworkContext.RegisterHandler(MsgType.Disconnect, OnPlayerDissConnect);
        NetworkContext.RegisterHandler(NetworkMessages.OpponentCard, OnOpponentCardReceived);
    }

    public int GetConnectionsCount
    {
        get { return NetworkServer.connections.Count; }
    }
}

