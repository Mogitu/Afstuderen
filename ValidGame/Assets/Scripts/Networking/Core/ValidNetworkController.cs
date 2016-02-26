using UnityEngine;
using UnityEngine.Networking;
using System;
using AMC.Networking;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Highlevel networkmanager
/// </summary>

public class ValidNetworkController : NetworkController
{
    public EventManager EventManager;
    public int SocketPort = 7777;

    private AmcClient Client;
    private AmcServer Server;
    private bool IsClient { get; set; }

    void Start()
    {
        EventManager.AddListener(EVENT_TYPE.SendSchat, SendChatMsgs);
        EventManager.AddListener(EVENT_TYPE.SendScoreNetwork, SendScoreMsgs);
        EventManager.AddListener(EVENT_TYPE.SendCardToOpponent, SendOpponentCard);
    }

    public void SendOpponentCard(short Event_Type, Component Sender, object param = null)
    {
        Debug.Log("Sending card");
        CardToOpponentMessage msgA = new CardToOpponentMessage();
        msgA.CardName = "jan";
      
        if (IsClient && Client != null)
        {
            Client.SendMessage(MsgTypes.MsgOnOpponentCardReceived, msgA);
        }
        else
        {
            Server.SendMessage(MsgTypes.MsgOnOpponentCardReceived, msgA);
        }
    }

    public void OnOpponentCardReceived(NetworkMessage msg)
    {
        CardToOpponentMessage msgA = msg.ReadMessage<CardToOpponentMessage>();
        Debug.Log("Card : "+msgA.CardName);
    }

    public override void BeginHosting()
    {
        Server = new AmcServer(SocketPort);
        Server.RegisterHandler(MsgTypes.MsgChat, OnChatMessageReceived);
        Server.RegisterHandler(MsgTypes.MsgScore, OnScoreMessageReceived);
        Server.RegisterHandler(MsgType.Connect, OnPlayerConnect);
        Server.RegisterHandler(MsgType.Disconnect, OnPlayerDissConnect);
        Server.RegisterHandler(MsgTypes.MsgOnOpponentCardReceived, OnOpponentCardReceived);
    }

    public override void StartClient(string ip)
    {
        Client = new AmcClient(ip, SocketPort);
        Client.RegisterHandler(MsgTypes.MsgChat, OnChatMessageReceived);
        Client.RegisterHandler(MsgTypes.MsgScore, OnScoreMessageReceived);
        Client.RegisterHandler(MsgType.Disconnect, OnPlayerDissConnect);
        Client.RegisterHandler(MsgTypes.MsgOnOpponentCardReceived, OnOpponentCardReceived);
        IsClient = true;
    }

    public void SendChatMsgs(short event_Type, Component sender, object param = null)
    {
        ChatMessage msgA = new ChatMessage();
        msgA.Text = param.ToString();
        if (IsClient && Client != null)
        {
            Client.SendMessage(MsgTypes.MsgChat, msgA);
        }
        else
        {
            Server.SendMessage(MsgTypes.MsgChat, msgA);
        }
    }

    public void SendScoreMsgs(short event_Type, Component sender, object param = null)
    {
        ScoreMessage msgA = new ScoreMessage();
        msgA.Score = (int)param;
        Debug.Log("sending as score to gui: " + msgA.Score);
        if (IsClient && Client != null)
        {
            Client.SendMessage(MsgTypes.MsgScore, msgA);
        }
        else
        {
            Server.SendMessage(MsgTypes.MsgScore, msgA);
        }
    }

    void OnChatMessageReceived(NetworkMessage msg)
    {
        ChatMessage msgA = msg.ReadMessage<ChatMessage>();
        Debug.Log("Received Chat");
        EventManager.PostNotification(EVENT_TYPE.ReceiveChatNetwork, this, msgA.Text.ToString());
    }

    void OnScoreMessageReceived(NetworkMessage msg)
    {
        ScoreMessage msgA = msg.ReadMessage<ScoreMessage>();
        Debug.Log("Received network score " + msgA.Score);
        EventManager.PostNotification(EVENT_TYPE.ReceiveScoreNetwork, this, msgA.Score);
    }

    void OnPlayerConnect(NetworkMessage msg)
    {
        EventManager.PostNotification(EVENT_TYPE.PlayerJoined, this, "JOINED");
        if (Server.ConnectionCount >= 2)
        {
            Debug.Log("Game is ready");
        }
    }

    void OnPlayerDissConnect(NetworkMessage msg)
    {
        EventManager.PostNotification(EVENT_TYPE.PlayerLeft, this, "LEFT");
    }

    public override void Disconnect()
    {
        // throw new NotImplementedException();
    }

    public override void ReceiveMessage()
    {
        //throw new NotImplementedException();
    }

    public override void SendMessage()
    {
        // throw new NotImplementedException();
    }

    public override void AddListeners()
    {
        throw new NotImplementedException();
    }

    public int GetConnectionsCount
    {
        get { return NetworkServer.connections.Count; }
    }
}

